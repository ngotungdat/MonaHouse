$(document).ready(function() {
    // if (Object.prototype.toString.call(window.HTMLElement).indexOf('Constructor') > 0) {
    //     console.log('có nè');
    //     $('input[type="file"]').removeAttr("multiple");
    // }

    $('#mainMenuOpen').on('click touchstart', function() {
        console.log('menu clicked');
    });

    $('body').on('click', function() {
        $('.off-canvas').removeClass('show');
    });
    $('.open-chat-nav').on('click', function(e) {
        e.stopPropagation();
        var target = $(this).attr('href');
        $(target).addClass('show');
    });
    $('.close-sidenav').on('click', function(e) {
        e.preventDefault();
        $(this).closest('.off-canvas').removeClass('show');
    });
    $('body').on('click', '.off-canvas', function(e) {
        e.preventDefault();
        e.stopPropagation();
        e.stopImmediatePropagation();
        return false;
    });

    //Drag tabble
    var elementScroll = document.querySelectorAll(".table-responsive");

    if (elementScroll != undefined || elementScroll != null) {
        elementScroll.forEach(function(element) {
            var mx = 0;
            element.addEventListener("mousedown", function(e) {
                this.sx = this.scrollLeft;
                mx = e.pageX - this.offsetLeft;

                this.addEventListener("mousemove", mouseMoveFunction);
            });
            element.addEventListener("mouseup", function(e) {
                this.removeEventListener("mousemove", mouseMoveFunction);
                mx = 0;
            });

            function mouseMoveFunction(e) {
                var mx2 = e.pageX - this.offsetLeft;
                if (mx) this.scrollLeft = this.sx + mx - mx2;
            }
        });
    }


    //detail info
    $("body").on("click", ".edit-mode", function(e) {
        e.preventDefault();
        $($(this).attr("data-target")).addClass("show");
        $(".detail-fixed").animate({ scrollTop: 0 }, "fast");
    });
    $("body").on("click", ".close-editmode,.bg-overlay", function(e) {
        $(this)
            .parents(".detail-fixed")
            .removeClass("show");
    });


    //Select2 in modal 

    $('.select2').each(function() {
        let $this = $(this);
        let $parent = $(this).closest('.modal');
        if ($parent.length > 0) {
            $this.select2({
                dropdownParent: $parent.find('.modal-body'),
            })
        } else {
            $this.select2();
        }
    });


    $('#scroll-to-top').on('click', function() {
        $('html,body').animate({
            scrollTop: 0
        }, 1000);
    });
    //datepicker
    $.datetimepicker.setLocale('en');
    $('.datetimepicker').each(function() {
        $(this).attr('autocomplete', 'off');
        $(this).datetimepicker({
            format: 'd/m/Y H:i',
            formatDate: 'd/m/Y',
            // theme:'dark',
            // defaultDate:new Date(),
            onShow: function(ct, element) {
                if ($(element).hasClass('to-date')) {
                    var minDate = $(element).closest('.from-to-group').find('.from-date').val();
                    this.setOptions({
                        minDate: minDate ? minDate : false,
                    })
                }
                if ($(element).hasClass('date-only')) {
                    this.setOptions({
                        timepicker: false,
                        format: 'd/m/Y',
                        mask: true,
                    })
                }
             
            }
        });
    })
    $('body').on('show.bs.dropdown', '.table-responsive', function() { $(this).css("overflow", "visible"); }).on('hide.bs.dropdown', '.table-responsive', function() { $(this).css("overflow", "auto"); });

    //sticky tinymce
    $('body').on('click touchstart', '.upload-box .btn-upload', function() {
        $(this).closest('.upload-box').find('input[type="file"]').click();
        $(this).closest('.upload-box').find('input[type="file"]').val('');
    });

    $('body').on('change', '.upload-image-input', function(event) {
        let input, files, $previewBox;
        input = event.target;
        files = input.files;
        let lightGalleryArr = [];
        $previewBox = $(input).closest('.upload-box').find('.preview-image');

        while ($previewBox.get(0).firstChild) {
            $previewBox.get(0).removeChild($previewBox.get(0).firstChild);
        }

        Array.from(files).map((file, index) => {
            let reader = new FileReader();
            let $imgBox = $('<div class="image-box lightgallery"></div');
            let $imgWrap = $('<div class="image-wrap"></div>');
            reader.onload = function(e) {
                const $img = $('<a href="' + e.target.result + '" class=""><img src="' + e.target.result + '" class="img" alt="image"></a>');
                const $delete = $('<a href="javascript:;" class="delete-image"><i class="fas fa-times"></i></a>');
                $imgBox.append($img);
                $imgWrap.append($delete);
                $imgWrap.append($imgBox);
                $previewBox.append($imgWrap);
            };
            reader.onloadend = function(e) {
                //initLightGallery();
            }
            reader.readAsDataURL(file);
        });
    });
    $(document).on('click', '.image-wrap .delete-image', function(e) {
        e.preventDefault();
        $(this).closest('.image-wrap').hide(500, function() {
            $(this).remove();
        })
    });
    $('.preview-image').on('click touchstart', '.image-box', function(e) {
        e.preventDefault();
        const $parent = $(this).closest('.preview-image');
        const $ele = $(this);
        let imgArr = [];
        let imageLists = $parent.find('.image-box img');
        Array.from(imageLists).map((img, index) => {
            imgArr[index] = {
                "src": img.getAttribute('data-img') ? img.getAttribute('data-img') : img.getAttribute('src'),
                "thumb": img.getAttribute('data-img') ? img.getAttribute('data-img') : img.getAttribute('src'),
                "subHtml": img.getAttribute('data-title') ? img.getAttribute('data-title') : ""
            }
        });

        if ($(this).data('lightGallery'))
            $(this).data('lightGallery').destroy(true);
        setTimeout(function() {
            $ele.lightGallery({
                dynamic: true,
                dynamicEl: imgArr,
                thumbnail: true,
                index: $ele.parent().index()
            });
        }, 250);
    });




    function initLightGallery() {
        $('.lightgallery').each((index, ele) => {
            $(ele).lightGallery({
                thumbnail: false,
                animateThumb: false,
                showThumbByDefault: false,
                fullScreen: false
            });

        });
    }
    initLightGallery();
    $('[data-toggle="popover"]').popover({
        html:true,
        hover:true
    });

    const printSelectContainer = (id)=> {
        let printContent = document.getElementById(id);
        console.log(id);
        let printSection = document.querySelector('.print-section');
        if (printSection !== null) {
            let contentWrap = printSection.querySelector('.print-content-dynamic');
            let contentPrintClone = printContent.cloneNode(true);
            while (contentWrap.firstChild) {
                contentWrap.removeChild(contentWrap.firstChild)
                ''
            }
            contentWrap.appendChild(contentPrintClone);
            window.focus();
            window.print();
            window.close();
        } else {
            return false;
        }
    }
    let printBtns = document.querySelectorAll('.btn-print');
    if(printBtns.length > 0){
        [...printBtns].map(ele => {
            ele.addEventListener('click', function(e) {
                e.preventDefault();
                console.log('btn print clicked');
                let secId = ele.dataset.print;
                printSelectContainer(secId);
            });
        })
    }
 
})