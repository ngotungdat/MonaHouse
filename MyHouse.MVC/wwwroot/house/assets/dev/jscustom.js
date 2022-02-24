(function (root, factory) {
    try {
        // commonjs
        if (typeof exports === 'object') {
            module.exports = factory();
            // global
        } else {
            root.toast = factory();
        }
    } catch (error) {
        console.log('Isomorphic compatibility is not supported at this time for toast.')
    }
})(this, function () {

    // We need DOM to be ready
    if (document.readyState === 'complete') {
        init();
    } else {
        window.addEventListener('DOMContentLoaded', init);
    }

    // Create toast object
    toast = {
        // In case toast creation is attempted before dom has finished loading!
        create: function () {
            console.error([
                'DOM has not finished loading.',
                '\tInvoke create method when DOM\s readyState is complete'
            ].join('\n'))
        }
    };
    var autoincrement = 0;

    // Initialize library
    function init() {
        // Toast container
        var container = document.createElement('div');
        container.id = 'cooltoast-container';
        document.body.appendChild(container);


        // Replace create method when DOM has finished loading
        toast.create = function (options) {
            var toast = document.createElement('div');
            toast.id = ++autoincrement;
            toast.id = 'toast-' + toast.id;
            toast.className = 'cooltoast-toast clear';
            var toastContent = document.createElement('div');
            toastContent.className = 'toast-content';
            // background
            if (options.classBackground) {
                toast.className += ' ' + options.classBackground;
            }
            // title
            if (options.title) {

                var h4 = document.createElement('h4');
                h4.className = 'cooltoast-title';
                h4.innerHTML = options.title;
                toastContent.appendChild(h4);
            }

            // text
            if (options.text) {
                var p = document.createElement('p');
                p.className = 'cooltoast-text';
                p.innerHTML = options.text;
                toastContent.appendChild(p);
            }

            // icon
            if (options.icon) {
                var icon = document.createElement('i');
                icon.innerHTML = options.icon;
                icon.className = 'cooltoast-icon material-icons';
                toast.appendChild(icon);
            }

            // click callback
            if (typeof options.callback === 'function') {
                toast.addEventListener('click', options.callback);
            }

            // toast api
            toast.hide = function () {
                toast.className += ' cooltoast-fadeOut';
                toast.addEventListener('animationend', removeToast, false);
            };

            // autohide
            if (options.timeout) {
                setTimeout(toast.hide, options.timeout);
            }
            // else setTimeout(toast.hide, 2000);

            if (options.type) {
                toast.className += ' cooltoast-' + options.type;
            }
            toast.appendChild(toastContent);
            toast.addEventListener('click', toast.hide);


            function removeToast() {
                var container = document.getElementById('cooltoast-container');
                container.removeChild(toast);
            }

            document.getElementById('cooltoast-container').appendChild(toast);
            return toast;
        }
    }
    return toast;

});
/*===========*/
$('body').on('change', '.upload-image-renter', function (event) {
    let input, files, $previewBox, $imgSubmit, $btnsaveimgInfo;
    input = event.target;
    files = input.files;
    $previewBox = $(input).closest('.upload-box').find('.preview-image');
    $imgSubmit = $(input).closest('.upload-box').find('.submit-image');
    $btnsaveimgInfo = $(input).closest('.upload-box').find('.btn-save-info-img');
    //while ($previewBox.get(0).firstChild) {
    //    $previewBox.get(0).removeChild($previewBox.get(0).firstChild);
    //}
    Array.from(files).map((file, index) => {
        let url;
        let id = 0;
        var formData = new FormData();
        formData.append("FileUpload", file);
        $.ajax({
            async: false,
            type: 'POST',
            url: '/Room/UploadFileIMG',
            data: formData,
            dataType: 'json',
            contentType: false,
            processData: false,
            success: function (msg) {
                if (msg.rs) {
                    url = msg.l;
                    id = msg.i;
                    let reader = new FileReader();
                    let $imgBox = $('<div class="image-box lightgallery"></div');
                    let $imgWrap = $('<div class="image-wrap"></div>');
                    reader.onload = function (e) {
                        const $img = $('<a href="' + url + '" class=""><img data-id="' + id + '" src="' + url + '" class="img" alt="image"></a>');
                        const $delete = $('<a href="javascript:;" class="delete-image"><i class="fas fa-times"></i></a>');
                        $imgBox.append($img);
                        $imgWrap.append($delete);
                        $imgWrap.append($imgBox);
                        $previewBox.append($imgWrap);
                        if ($imgSubmit != undefined) {
                            $imgSubmit.val(id);
                        }
                        if ($btnsaveimgInfo != undefined) {
                            $($btnsaveimgInfo).removeClass("hidden");
                        }
                    };
                    reader.onloadend = function (e) {
                        //initLightGallery();
                    }
                    reader.readAsDataURL(file);
                }
                else {
                    toast.create({
                        title: 'Notification!',
                        text: msg.message,
                        icon: 'error_outline',
                        classBackground: 'noti-error',
                        timeout: 3000
                    });
                }

            },
            error: function (error) {
                console.log('e');
            }
        });

    });
});

$('.preview-image').on('click touchstart', '.image-box', function (e) {
    e.preventDefault();
    const $parent = $(this).closest('.preview-image');
    const $ele = $(this);
    let imgArr = [];
    let imageLists = $parent.find('.image-box img');
    Array.from(imageLists).map((img, index) => {
        imgArr[index] = {
            "src": $(img).parent().attr('href') ? $(img).parent().attr('href') : img.getAttribute('src'),
            "thumb": img.getAttribute('data-img') ? img.getAttribute('data-img') : img.getAttribute('src'),
            "subHtml": img.getAttribute('data-title') ? img.getAttribute('data-title') : ""
        }
    });

    if ($(this).data('lightGallery'))
        $(this).data('lightGallery').destroy(true);
    setTimeout(function () {
        $ele.lightGallery({
            dynamic: true,
            dynamicEl: imgArr,
            thumbnail: true,
            index: $ele.parent().index()
        });
    }, 250);
});

$(document).on('click', '.image-wrap .delete-image', function (e) {
    e.preventDefault();
    $(this).parent().parent().parent().find('input.submit-image').val('');
    $(this).closest('.image-wrap').hide(500, function () {
        $(this).remove();
    })
});
