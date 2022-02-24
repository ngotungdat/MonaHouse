import { replaceInputType, calculateDropdownPos, resizableGrid } from './dat-helper.js';
$(document).ready(function() {
    $("body").on("input", 'input', function() {
        let type = $(this).attr('data-type');
        let value = $(this).val();
        if (type !== '' && type !== undefined) {
            $(this).val(replaceInputType(type, value));
        }
    });

    function toggleSort(ele) {
        const $this = $(ele);
        if ($this.attr('data-sort') === undefined || $this.attr('data-sort') === '') {
            $this.attr('data-sort', 'ASC');
        }

        if ($this.find('.fa-sort').length > 0)
            $this.find('.fa').removeClass('fa-sort');

        let sortState = $this.attr('data-sort');
        switch (sortState) {
            case 'ASC':
                {
                    $this.find('.fa').addClass('fa-sort-down');
                    $this.find('.fa').removeClass('fa-sort-up');
                    $this.attr('data-sort', 'DESC');
                    $(this).closest('tr').find('.fa').not($this.find('.fa')).removeAttr('class').attr('class', 'fa fa-sort');
                    $(this).closest('tr').find('.sort-label').not($this).removeAttr('data-sort');
                    break;
                }
            case 'DESC':
                {
                    $this.find('.fa').addClass('fa-sort-up');
                    $this.find('.fa').removeClass('fa-sort-down');
                    $this.attr('data-sort', 'ASC');
                    $(this).closest('tr').find('.fa').not($this.find('.fa')).removeAttr('class').attr('class', 'fa fa-sort');
                    $(this).closest('tr').find('.sort-label').not($this).removeAttr('data-sort');
                    break;
                }
        }
    }
    $('.sort-label').on('click', function(e) {
        e.preventDefault();
        e.stopPropagation();
        toggleSort(this);
    });

    $('body').on('click', '.table-action', function(e) {
        e.preventDefault();
        e.stopPropagation();
        $('.table-action').not($(this)).removeClass('active');
        $(this).toggleClass('active');
        calculateDropdownPos(this);
    });

    $('body').on('click', function(e) {
        const $source = $(e.target);
        if (!$source.is('.table__head-action')) {
            $('.table-action').removeClass('active');
        }
    });

    function checkboxStatus($element) {
        const $this = $element;
        let checkedBox = $this.closest('.table__filter-wrap ').find('tbody .checkbox-col input[type="checkbox"]:checked');
        let checkbox = $this.closest('.table__filter-wrap ').find('tbody .checkbox-col input[type="checkbox"]');
        let checkboxActions = $this.closest('.table__filter-wrap ').find('.table__filter-head .checkbox__action');
        if (checkedBox.length > 0) {
            checkboxActions.addClass('active');
        } else {
            checkboxActions.removeClass('active');

        }
        if (checkedBox.length === checkbox.length) {
            $this.closest('table').find('.checkbox-all').prop('checked', true);
        } else {
            $this.closest('table').find('.checkbox-all').prop('checked', false);
        }
        checkboxActions.find('.check-num').text(checkedBox.length);
    }

    $('.table__filter-wrap tbody').on('change', '.checkbox-col input[type="checkbox"]', function() {
        checkboxStatus($(this));
    });

    $('.table__filter-wrap .checkbox-col .checkbox-all').on('change', function() {
        var checked = $(this).prop('checked');
        $(this).closest('.table').find('tbody .checkbox-col input[type="checkbox"]').prop('checked', checked);
        checkboxStatus($(this));
    });

    $('.table__filter__wrap').on('click', '.js-toggle-filter', function(e) {
        e.preventDefault();
        $(this).toggleClass('active');
        $(this).closest('.table__filter__wrap').find('.table__filter-form').stop().slideToggle(300);
    });
    $('.table__filter__wrap').on('click', '.toggle-collapse', function() {
        $(this).toggleClass('active');
        let $closestRow = $(this).closest('tr').nextAll('.collapse-row').first();
      
        $closestRow.stop().fadeToggle(200);
        console.log($closestRow);
    });

    $('.height-fixed').on('scroll', function() {
        let scrollTop = $(this).scrollTop();
        let tableFixedHead = $(this).find('.fixed-header');
        let firstHeader = $(this).find(' thead tr:first-child');
        if (scrollTop > 0 && $(this).find('.tr-search').length > 0) {
            $(this).find('.tr-search th').css({
                'box-shadow': '0px 6px 5px -3px rgba(0,0,0,.15)',
                'top': firstHeader.height()
            });
        } else {
            $(this).find('.tr-search th').attr('style', '');
        }
    });



    $('.js-customize-column').on('click', function(e) {
        e.preventDefault();
        $('.pop__up').addClass('show');
        filterTable($(this).attr('data-target'));
    });
    $('body').on('click', '.cancel-popup,.bg-overlay', function() {
        $('.pop__up').removeClass('show');
    });

    function filterTable(tableId) {
        let filterField;
        let table = document.getElementById(tableId);
        
        if(table === null)
        return false;
    
        if (table.hasAttribute('data-hidden')) {
            filterField = JSON.parse(table.dataset.hidden);
        } else {
            filterField = '[]';
        }


        let trTd = table.querySelectorAll('tr td, tr th');
        let listItemFilters = Array.from(trTd).map(element => {
            let dataColumn = element.getAttribute('data-column');
            element.classList.remove('hidden');
            if (filterField.indexOf(dataColumn) > -1) {
                element.classList.add('hidden');
            }
        });
        let listTh = table.querySelectorAll('.sort-label');
        let template = getHTMLTemplate(listTh);
        let popup = document.querySelector('.customize-popup');
        if (popup !== undefined && popup !== null) {
            let listFilterCheckbox = popup.querySelector('.list__column');
            listFilterCheckbox.innerHTML = template;
            let listCheckbox = listFilterCheckbox.querySelectorAll('.column-item input[type="checkbox"');
            popup.setAttribute('data-id', tableId);
            try {

            } catch (e) {
                console.log(e);
            }
        }
    }

    function applyFilter() {

        let dataFilter = [];
        let popup = document.querySelector('.customize-popup');
        let tableId = popup.getAttribute('data-id');
        let listCheckbox = popup.querySelectorAll('.column-item input[type="checkbox"');
        Array.from(listCheckbox).map(checkbox => {
            if (!checkbox.checked) {
                dataFilter.push(checkbox.value);
            }
        });
        console.log(dataFilter);
        document.getElementById(tableId).setAttribute('data-hidden', JSON.stringify(dataFilter));
        filterTable(tableId);
        popup.classList.remove('show');

    }

    function getHTMLTemplate(filterTh) {
        let html = '';
        filterTh.forEach(th => {
            html += ` <li class="column-item">
            <div class="custom-checkbox">
                <label>
                    <input type="checkbox" value="${th.getAttribute('data-column')}"  ${th.classList.contains('hidden')?'':'checked'}>
                    <span>${th.innerText}</span>
                </label>
            </div>
        </li>`;
        });
        return html;
    }

    const tables = document.getElementsByTagName('table');
    let tableFilter = [].slice.call(tables).filter(table => [].slice.call(table.classList).indexOf('fixed-header') === -1);
    tableFilter.map(table => {
        if (table.getAttribute('id') !== null) {
            filterTable(table.getAttribute('id'));
        }

    });
    let tb = document.getElementById('js-apply-filter');
    if (tb) {
        tb.addEventListener('click', applyFilter);
    }


});