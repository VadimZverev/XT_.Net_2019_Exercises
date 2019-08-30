function addToSelected() {

    let object = event.target;

    do {
        object = object.parentElement;
        var name = object.localName + '.' + object.className;
    } while (name !== "div.main");

    var availableDOM = object.querySelector("div.available select");
    var selectedDOM = object.querySelector("div.selected select");

    while (availableDOM.selectedOptions.length != 0) {
        selectedDOM.appendChild(availableDOM.selectedOptions[0]);
    };

    while (selectedDOM.selectedOptions.length != 0) {
        selectedDOM.selectedOptions[0].selected = false;
    }

    let button = object.querySelector("#addAllToAvailable");

    if (availableDOM.options.length == 0) {

        event.target.setAttribute("disabled", "");
        button.removeAttribute("disabled");

        button = object.querySelector("#addAllToSelected");
        button.setAttribute("disabled", "");

        button = object.querySelector("#addToAvailable");
        button.removeAttribute("disabled");

    } else if (availableDOM.options.length > 0) {

        if (event.target.hasAttribute("disabled")) {
            event.target.removeAttribute("disabled");
        }

        if (button.hasAttribute("disabled")) {
            button.removeAttribute("disabled");
        }

        button = object.querySelector("#addAllToSelected");

        if (button.hasAttribute("disabled")) {
            button.removeAttribute("disabled");
        }

        button = object.querySelector("#addToAvailable");

        if (button.hasAttribute("disabled")) {
            button.removeAttribute("disabled");
        }
    }
}

function addToAvailable() {

    let object = event.target;

    do {
        object = object.parentElement;
        var name = object.localName + '.' + object.className;
    } while (name !== "div.main");

    var selectedDOM = object.querySelector("div.selected select");
    var availableDOM = object.querySelector("div.available select");

    while (selectedDOM.selectedOptions.length != 0) {
        availableDOM.appendChild(selectedDOM.selectedOptions[0]);
    };

    while (availableDOM.selectedOptions.length != 0) {
        availableDOM.selectedOptions[0].selected = false;
    }

    let button = object.querySelector("#addAllToSelected");

    if (button.hasAttribute("disabled")) {
        button.removeAttribute("disabled");
    }

    if (selectedDOM.options.length == 0) {

        event.target.setAttribute("disabled", "");
        button.removeAttribute("disabled");

        button = object.querySelector("#addAllToAvailable");
        button.setAttribute("disabled", "");

        button = object.querySelector("#addToSelected");
        button.removeAttribute("disabled");

    } else if (selectedDOM.options.length > 0) {

        if (event.target.hasAttribute("disabled")) {
            event.target.removeAttribute("disabled");
        }

        if (button.hasAttribute("disabled")) {
            button.removeAttribute("disabled");
        }

        button = object.querySelector("#addAllToAvailable");

        if (button.hasAttribute("disabled")) {
            button.removeAttribute("disabled");
        }

        button = object.querySelector("#addToSelected");

        if (button.hasAttribute("disabled")) {
            button.removeAttribute("disabled");
        }
    }
}

function addAllToAvailable() {
    let object = event.target;

    do {
        object = object.parentElement;
        var name = object.localName + '.' + object.className;
    } while (name !== "div.main");

    var selectedDOM = object.querySelector("div.selected select");
    var availableDOM = object.querySelector("div.available select");

    while (selectedDOM.options.length != 0) {
        availableDOM.appendChild(selectedDOM.options[0]);
    };

    while (availableDOM.selectedOptions.length != 0) {
        availableDOM.selectedOptions[0].selected = false;
    }

    event.target.setAttribute("disabled", "");
    object.querySelector("#addToAvailable").setAttribute("disabled", "");

    object.querySelector("#addAllToSelected").removeAttribute("disabled");
    object.querySelector("#addToSelected").removeAttribute("disabled");
}

function addAllToSelected() {

    let object = event.target;

    do {
        object = object.parentElement;
        var name = object.localName + '.' + object.className;
    } while (name !== "div.main");

    var availableDOM = object.querySelector("div.available select");
    var selectedDOM = object.querySelector("div.selected select");

    while (availableDOM.options.length != 0) {
        selectedDOM.appendChild(availableDOM.options[0]);
    };

    while (selectedDOM.selectedOptions.length != 0) {
        selectedDOM.selectedOptions[0].selected = false;
    }

    event.target.setAttribute("disabled", "");
    object.querySelector("#addToSelected").setAttribute("disabled", "");
    
    object.querySelector("#addAllToAvailable").removeAttribute("disabled");
    object.querySelector("#addToAvailable").removeAttribute("disabled");
}