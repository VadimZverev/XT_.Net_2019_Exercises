function mathCalculator() {

    var result;
    var mathExpression = document.getElementById("expression")
        .value.match(/\d+(\.\d+)?|[+\-*/=]/g);

    if (mathExpression === null){
        document.getElementById("result").value = 'Enter expression!';
        return;
    }

    if (!isCorrectInput(mathExpression)) {
        document.getElementById("result").value = 'Incorrect input';
        return;
    };

    result = mathExpression[0];

    for (let i = 1; i + 2 < mathExpression.length; i += 2) {

        let x = Number(result);
        let y = Number(mathExpression[i + 1]);

        switch (mathExpression[i]) {
            case "+": {
                result = (x + y).toFixed(2);
                break;
            }
            case "-": {
                result = (x - y).toFixed(2);
                break;
            }

            case "*": {
                result = (x * y).toFixed(2);
                break;
            }

            case "/": {
                y !== 0
                    ?
                    result = (x / y).toFixed(2)
                    :
                    result = 0;
                break;
            }
        }
    }

    document.getElementById("result").value = result;
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