let addToSelectedBtn = document.getElementById("addToSelected");
let addToAvailableBtn = document.getElementById("addToAvailable");
let updateForm = document.getElementById("updateUser");

updateForm.onsubmit = selectAllBeforeSendData;
addToSelectedBtn.addEventListener('click', addToSelected);
addToAvailableBtn.addEventListener('click', addToAvailable);

function addToSelected() {

    let availableDom = document.getElementById("availableAwards");

    if (!isSelectedOptions(availableDom)) return;

    let selectedDom = document.getElementById("selectedAwards");

    moveToSelect(availableDom, selectedDom);

    if (availableDom.length === 0) {
        toggleBtnAttrDisabled(addToAvailableBtn, addToSelectedBtn);
    }
    else {
        toggleBtnAttrDisabled(addToAvailableBtn);
        toggleBtnAttrDisabled(addToSelectedBtn);
    }
}

function addToAvailable() {

    let selectedDom = document.getElementById("selectedAwards");

    if (!isSelectedOptions(selectedDom)) return;

    let availableDom = document.getElementById("availableAwards");

    moveToSelect(selectedDom, availableDom);

    if (selectedDom.length === 0) {
        toggleBtnAttrDisabled(addToSelectedBtn, addToAvailableBtn);
    }
    else {
        toggleBtnAttrDisabled(addToAvailableBtn);
        toggleBtnAttrDisabled(addToSelectedBtn);
    }
}

function toggleBtnAttrDisabled(btnOffDisabled, btnOnDisabled) {

    if (btnOffDisabled) {
        if (btnOffDisabled.hasAttribute("disabled")) {
            btnOffDisabled.removeAttribute("disabled");
        }
    }

    if (btnOnDisabled) {
        if (!btnOnDisabled.hasAttribute("disabled")) {
            btnOnDisabled.setAttribute("disabled", "");
        }
    }
}

function moveToSelect(fromSelect, toSelect) {

    while (fromSelect.selectedOptions.length !== 0) {
        toSelect.appendChild(fromSelect.selectedOptions[0]);
    }

    while (toSelect.selectedOptions.length !== 0) {
        toSelect.selectedOptions[0].selected = false;
    }
}

function isSelectedOptions(select) {
    return select.selectedOptions.length !== 0;
}

function selectAllBeforeSendData() {
    let selectedDom = document.getElementById("selectedAwards");

    for (let i = 0; i < selectedDom.length; i++) {
        selectedDom[i].selected = true;
    }

    return true;
}