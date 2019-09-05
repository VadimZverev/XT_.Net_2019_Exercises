const pattern = new RegExp(/\d+(\.\d+)?|[+\-*/=]/g);

var execute = document.getElementById("execute");
var expressionInput = document.getElementById("expression");
var resultInput = document.getElementById("result");

execute.addEventListener('click', mathCalculator);
expressionInput.addEventListener('keyup', () => {
    if (event.keyCode === 13) {
        execute.click();
    }
});

function mathCalculator() {
    let mathExpression = expressionInput.value.match(pattern);

    if (mathExpression === null) {
        resultInput.value = 'Enter expression!';
        return;
    }

    if (!isCorrectInput(mathExpression)) {
        resultInput.value = 'Incorrect input';
        return;
    }

    resultInput.value = executeExpression(mathExpression);
}

function isCorrectInput(input) {

    if (input[input.length - 1] !== '=') {
        return false;
    }

    if (input.lastIndexOf('=', input.length - 2) !== -1) {
        return false;
    }

    for (let i = 0; i < input.length; i++) {

        let isNan = isNaN(input[i]);
        let isEven = i % 2 === 0;

        if (isEven && isNan) {
            return false;
        }

        if (!isEven && !isNan) {
            return false;
        }
    }

    return true;
}

function executeExpression(expression) {
    let res = +expression[0];

    for (let i = 1; i + 2 < expression.length; i += 2) {

        let y = +expression[i + 1];

        switch (expression[i]) {
            case "+": {
                res += y;
                break;
            }
            case "-": {
                res -= y;
                break;
            }

            case "*": {
                res *= y;
                break;
            }

            case "/": {
                y !== 0
                    ?
                    res /= y
                    :
                    res = 0;
                break;
            }
        }
    }

    return res.toFixed(2);
}