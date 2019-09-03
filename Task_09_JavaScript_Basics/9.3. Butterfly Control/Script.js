window.onload = setSizeSelect();

var inputsControl = document.querySelectorAll("div.control input");
inputsControl.forEach((elem) => {

    if (elem.dataset.action == "addAllToSelected") {
        elem.addEventListener('click', addAllToSelected);
    }
    else if (elem.dataset.action == "addAllToAvailable") {
        elem.addEventListener('click', addAllToAvailable);
    }
    else if (elem.dataset.action == "addToSelected") {
        elem.addEventListener('click', addToSelected);
    }
    else if (elem.dataset.action == "addToAvailable") {
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
        let offDisabled = ["addToAvailable", "addAllToAvailable"];
        let onDisabled = ["addAllToSelected", "addToSelected"];

        toggleBtnAttrDisabled(btnsControl, offDisabled, onDisabled);
    }
    else {
        let offDisabled = ["addToAvailable", "addAllToSelected", "addToSelected", "addAllToAvailable"];
        toggleBtnAttrDisabled(btnsControl, offDisabled);
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
        let offDisabled = ["addAllToSelected", "addToSelected"];
        let onDisabled = ["addToAvailable", "addAllToAvailable"];

        toggleBtnAttrDisabled(btnsControl, offDisabled, onDisabled);
    }
    else {
        let offDisabled = ["addToAvailable", "addAllToSelected", "addToSelected", "addAllToAvailable"];
        toggleBtnAttrDisabled(btnsControl, offDisabled);
    }
}

function addAllToAvailable() {

    let object = event.target.closest("div.main");

    if (!object) return;
    
    let selectedDom = object.querySelector("div.selected select");
    let availableDom = object.querySelector("div.available select");
    moveAllToSelect(selectedDom, availableDom);
    
    let offDisabled = ["addAllToSelected", "addToSelected"];
    let onDisabled = ["addToAvailable", "addAllToAvailable"];
    let btnsControl = event.target.closest("div.control").children;

    toggleBtnAttrDisabled(btnsControl, offDisabled, onDisabled);
}

function addAllToSelected() {

    let object = event.target.closest("div.main");

    if (!object) return;

    let availableDom = object.querySelector("div.available select");
    let selectedDom = object.querySelector("div.selected select");
    moveAllToSelect(availableDom, selectedDom);

    let onDisabled = ["addAllToSelected", "addToSelected"];
    let offDisabled = ["addToAvailable", "addAllToAvailable"];
    let btnsControl = event.target.closest("div.control").children;

    toggleBtnAttrDisabled(btnsControl, offDisabled, onDisabled);
}

function toggleBtnAttrDisabled(btns, offDisabled, onDisabled) {

    for (let i = 0; i < btns.length; i++) {

        let action = btns[i].dataset.action;

        if (onDisabled) {
            if (onDisabled.includes(action)) {
                if (!btns[i].hasAttribute("disabled")) {
                    btns[i].setAttribute("disabled", "");
                }
            }
        }
        if (offDisabled) {
            if (offDisabled.includes(action)) {
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