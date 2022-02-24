export const nonAccentVietnamese = (str) => {
    str = str.toLowerCase();
    str = str.replace(/\u00E0|\u00E1|\u1EA1|\u1EA3|\u00E3|\u00E2|\u1EA7|\u1EA5|\u1EAD|\u1EA9|\u1EAB|\u0103|\u1EB1|\u1EAF|\u1EB7|\u1EB3|\u1EB5/g, "a");
    str = str.replace(/\u00E8|\u00E9|\u1EB9|\u1EBB|\u1EBD|\u00EA|\u1EC1|\u1EBF|\u1EC7|\u1EC3|\u1EC5/g, "e");
    str = str.replace(/\u00EC|\u00ED|\u1ECB|\u1EC9|\u0129/g, "i");
    str = str.replace(/\u00F2|\u00F3|\u1ECD|\u1ECF|\u00F5|\u00F4|\u1ED3|\u1ED1|\u1ED9|\u1ED5|\u1ED7|\u01A1|\u1EDD|\u1EDB|\u1EE3|\u1EDF|\u1EE1/g, "o");
    str = str.replace(/\u00F9|\u00FA|\u1EE5|\u1EE7|\u0169|\u01B0|\u1EEB|\u1EE9|\u1EF1|\u1EED|\u1EEF/g, "u");
    str = str.replace(/\u1EF3|\u00FD|\u1EF5|\u1EF7|\u1EF9/g, "y");
    str = str.replace(/\u0111/g, "d");
    str = str.replace(/\u0300|\u0301|\u0303|\u0309|\u0323/g, "");
    str = str.replace(/\u02C6|\u0306|\u031B/g, "");
    return str;
}

export const replaceInputType = (type, value) => {
    switch (type) {
        case 'currency':
            {
                let regx = /\D+/g;
                let number = value.replace(regx, "");
                return number.replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.");
                break;
            }
        case 'number':
            {
                return value.replace(/[^0-9]/, "");
                break;
            }
    }
}
export const calculateDropdownPos = (element) => {
    let dropdown = element.nextElementSibling;
    let dropWidth = dropdown.offsetWidth;
    let pageScrollY = element.scrollTop;
    let clientRect = element.getBoundingClientRect();
    let eLeft = clientRect.left;
    let eTop = clientRect.top - pageScrollY;
    if (window.innerHeight - eTop < dropdown.offsetHeight) {
        dropdown.style.top = `${eTop - dropdown.offsetHeight}px`;
    } else {
        dropdown.style.top = `${eTop}px`;
    }
    if (eLeft < dropWidth) {
        dropdown.style.left = `${eLeft}px`;
        // dropdown.setAttribute('style', `
        //  left:${eLeft}px;
        //  top:${eTop}px;
        //  `);
    } else {
        dropdown.style.left = `${eLeft - dropWidth + element.offsetWidth}px`;
        // dropdown.setAttribute('style', `
        // left:${eLeft - dropWidth + element.offsetWidth}px;
        // top:${eTop}px;
        // `);
    }


}

export const resizableGrid = (table) => {
    let row = table.getElementsByTagName('tr')[0],
        cols = row ? row.children : undefined;
    if (!cols) return;

    table.style.overflow = 'hidden';

    let tableHeight = table.offsetHeight;

    for (let i = 0; i < cols.length; i++) {
        let div = createDiv(tableHeight);
        cols[i].appendChild(div);
        cols[i].style.position = 'relative';
        setListeners(div);
    }

    function setListeners(div) {
        let pageX, curCol, nxtCol, curColWidth, nxtColWidth;

        div.addEventListener('mousedown', function(e) {
            curCol = e.target.parentElement;
            nxtCol = curCol.nextElementSibling;
            pageX = e.pageX;

            let padding = paddingDiff(curCol);

            curColWidth = curCol.offsetWidth - padding;
            if (nxtCol)
                nxtColWidth = nxtCol.offsetWidth - padding;
        });

        div.addEventListener('mouseover', function(e) {
            e.target.style.borderRight = '2px solid #e1e1e1';
        })

        div.addEventListener('mouseout', function(e) {
            e.target.style.borderRight = '';
        })

        document.addEventListener('mousemove', function(e) {
            if (curCol) {
                let diffX = e.pageX - pageX;

                if (nxtCol)
                    nxtCol.style.width = (nxtColWidth - (diffX)) + 'px';

                curCol.style.width = (curColWidth + diffX) + 'px';
            }
        });

        document.addEventListener('mouseup', function(e) {
            curCol = undefined;
            nxtCol = undefined;
            pageX = undefined;
            nxtColWidth = undefined;
            curColWidth = undefined
        });
    }

    function createDiv(height) {
        let div = document.createElement('div');
        div.style.top = 0;
        div.style.right = 0;
        div.style.width = '5px';
        div.style.position = 'absolute';
        div.style.cursor = 'col-resize';
        div.style.userSelect = 'none';
        div.style.height = height + 'px';
        return div;
    }

    function paddingDiff(col) {

        if (getStyleVal(col, 'box-sizing') == 'border-box') {
            return 0;
        }

        let padLeft = getStyleVal(col, 'padding-left');
        let padRight = getStyleVal(col, 'padding-right');
        return (parseInt(padLeft) + parseInt(padRight));

    }

    function getStyleVal(elm, css) {
        return (window.getComputedStyle(elm, null).getPropertyValue(css))
    }
};