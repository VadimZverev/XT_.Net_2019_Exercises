window.onload = setSizeSelect();

var inputsControl = document.querySelectorAll("div.control input");
inputsControl.forEach((elem) => {

    if (elem.id == "addAllToSelected") {
        elem.addEventListener('click', addAllToSelected);
    }
    else if (elem.id == "addAllToAvailable") {
        elem.addEventListener('click', addAllToAvailable);
    }
    else if (elem.id == "addToSelected") {
        elem.addEventListener('click', addToSelected);
    }
    else if (elem.id == "addToAvailable") {
        elem.addEventListener('click', addToAvailable);
    }
});

function addToSelected() {

    let object = event.target.closest("div.main");

    if (!object) return;

    let availableDom = object.querySelector("div.available select");

    if (!isSelectedOptions(availableDom)) return;

    let selectedDom = object.querySelector("div.selected select");

    moveToSelect(availableDom, selectedDom);
    
    let btnsControl = event.target.closest("div.control").children;

    if (availableDom.length == 0) {
        let setIdAray = ["addAllToSelected", "addToSelected"];
        let removeIdAray = ["addToAvailable", "addAllToAvailable"];

        toggleBtnAttrDisabled(btnsControl, removeIdAray, setIdAray);
    }
    else {
        let removeIdAray = ["addToAvailable", "addAllToSelected", "addToSelected", "addAllToAvailable"];
        toggleBtnAttrDisabled(btnsControl, removeIdAray);
    }
}

function addToAvailable() {

    let object = event.target.closest("div.main");

    if (!object) return;

    let selectedDom = object.querySelector("div.selected select");

    if (!isSelectedOptions(selectedDom)) return;

    let availableDom = object.querySelector("div.available select");

    moveToSelect(selectedDom, availableDom);
    
    let btnsControl = event.target.closest("div.control").children;

    if (selectedDom.length == 0) {
        let removeIdAray = ["addAllToSelected", "addToSelected"];
        let setIdAray = ["addToAvailable", "addAllToAvailable"];

        toggleBtnAttrDisabled(btnsControl, removeIdAray, setIdAray);
    }
    else {
        let removeIdAray = ["addToAvailable", "addAllToSelected", "addToSelected", "addAllToAvailable"];
        toggleBtnAttrDisabled(btnsControl, removeIdAray);
    }
}

function addAllToAvailable() {

    let removeIdAray = ["addAllToSelected", "addToSelected"];
    let setIdAray = ["addToAvailable", "addAllToAvailable"];

    let object = event.target.closest("div.main");
    if (!object) return;

    let selectedDom = object.querySelector("div.selected select");
    let availableDom = object.querySelector("div.available select");
    moveAllToSelect(selectedDom, availableDom);

    let btnsControl = event.target.closest("div.control").children;
    toggleBtnAttrDisabled(btnsControl, removeIdAray, setIdAray);
}

function addAllToSelected() {

    let setIdAray = ["addAllToSelected", "addToSelected"];
    let removeIdAray = ["addToAvailable", "addAllToAvailable"];
    let object = event.target.closest("div.main");

    if (!object) return;

    let availableDom = object.querySelector("div.available select");
    let selectedDom = object.querySelector("div.selected select");
    moveAllToSelect(availableDom, selectedDom);

    let btnsControl = event.target.closest("div.control").children;
    toggleBtnAttrDisabled(btnsControl, removeIdAray, setIdAray);
}

function toggleBtnAttrDisabled(btns, removeIdArray, setIdArray) {

    for (let i = 0; i < btns.length; i++) {

        let _id = btns[i].id;

        if (setIdArray) {
            if (setIdArray.includes(_id)) {
                if (!btns[i].hasAttribute("disabled")) {
                    btns[i].setAttribute("disabled", "");
                }
            }
        }
        if (removeIdArray) {
            if (removeIdArray.includes(_id)) {
                if (btns[i].hasAttribute("disabled")) {
                    btns[i].removeAttribute("disabled");
                }
            }
        }
    }
}

function moveAllToSelect(fromSelect, toSelect) {

    while (fromSelect.length != 0) {

        if (fromSelect[0].selected) {
            fromSelect[0].selected = false;
        }

        toSelect.appendChild(fromSelect[0]);
    };
}

function moveToSelect(fromSelect, toSelect) {

    while (fromSelect.selectedOptions.length != 0) {
        toSelect.appendChild(fromSelect.selectedOptions[0]);
    };

    while (toSelect.selectedOptions.length != 0) {
        toSelect.selectedOptions[0].selected = false;
    }
}

function isSelectedOptions(select) {
    if (select.selectedOptions.length == 0)
        return false;
    else {
        return true;
    }
}

function setSizeSelect() {
    let selectsList = document.getElementsByTagName("select");
    let sizeList = 0;

    for (let i = 0; i < selectsList.length; i++) {

        let length = selectsList[i].length;

        if (sizeList < length) {
            sizeList = length;
        }
    }

    for (let i = 0; i < selectsList.length; i++) {
        selectsList[i].setAttribute("size", sizeList);
    }
}