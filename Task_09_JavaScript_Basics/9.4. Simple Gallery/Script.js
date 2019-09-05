const COLOR_RED = "#F00";

var isPause = false;
var prev;
var seconds = 9;
var swichMode;
var timer = document.getElementById("timer");

var stopwatch = setInterval(() => {
    if (!isPause) {
        timer.innerHTML = seconds;
        seconds--;

        if (seconds == 3) {
            timer.style.color = COLOR_RED;
        }

        if (seconds < 0) {
            clearInterval(stopwatch);
            endCountdown();
        }
    }
}, 1000);

window.onload = () => {
    prev = document.getElementById('prev');
    swichMode = document.getElementById("switchMode");

    if (prev) prev.addEventListener('click', goToPrevPage);
    if (swichMode) swichMode.addEventListener('click', toggleCountdown);
};

function endCountdown() {
    let pageName = getCurrentPageName(window.location);

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
    if (confirm("Start over?\nYes - start over; No - Watch Over.")) {
        window.location.href = "Page1.html";
    }
    else {
        let child = document.createElement("div");
        child.innerHTML = "<h1>Watch Over</h1>";
        child.style.color = COLOR_RED;

        document.body.innerHTML = child.outerHTML;
        document.title = "Watch Over";
    }
}

function goToNextPage(pageName) {
    let nextPage = "Page" + (+(pageName.slice(-1)) + 1) + ".html";
    window.location = nextPage;
}

function goToPrevPage() {
    let pageName = getCurrentPageName(window.location);

    let nextPageNumber = +(pageName.slice(-1)) - 1;
    window.location = "Page" + nextPageNumber + ".html";
}

function getCurrentPageName(location) {
    return location.href.match(/Page\d/g)[0];
}