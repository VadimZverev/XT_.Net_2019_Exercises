var isPause = false;
var seconds = 9;
var prev;
var swichMode;

window.onload = () => {
    swichMode = document.getElementById("switchMode");
    prev = document.getElementById('prev');

    if (swichMode) swichMode.addEventListener('click', toggleCountdown);
    if (prev) prev.addEventListener('click', goToPrevPage);
};

var stopwatch = setInterval(() => {
    if (!isPause) {
        document.getElementById("timer").innerHTML = seconds;
        seconds--;

        if (seconds < 0) {
            clearInterval(stopwatch);
            endCountdown();
        }
    }
}, 1000);

function endCountdown() {
    var pageName = getCurrentPageName(window.location);

    if (pageName == "Page4") {
        isContinue();
    }
    else {
        goToNextPage(pageName);
    }
}

function toggleCountdown() {
    let btn = event.target;

    if (btn.value == "pause") {
        isPause = true;
        btn.value = "resume";
    }
    else if (btn.value == "resume") {
        isPause = false;
        btn.value = "pause";
    }
}

function isContinue() {
    if (confirm("Start over?\nYes - start over; No - Watch Over.") == true) {
        window.location.href = "Page1.html";
    }
    else {
        var child = document.createElement("div");
        child.innerHTML = "<h1>Watch Over</h1>";
        child.setAttribute("style", "color:red;");

        document.body.innerHTML = child.outerHTML;
        document.title = "Watch Over";
    }
}

function goToNextPage(pageName) {
    var nextPage = "Page" + (+(pageName.slice(-1)) + 1) + ".html";
    window.location = nextPage;
}

function goToPrevPage() {
    var pageName = getCurrentPageName(window.location);

    var nextPageNumber = +(pageName.slice(-1)) - 1;
    window.location = "Page" + nextPageNumber + ".html";
}

function getCurrentPageName(location) {
    return location.href.match(/Page\d/g)[0];
}