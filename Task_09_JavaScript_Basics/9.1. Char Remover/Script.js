const isDEBUG = true;
const separators = ["\t", "?", "!", ":", ";", ",", "."];

var execute = document.getElementById("execute");
var result = document.getElementById("result");
var sentence = document.getElementById("sentence");

execute.addEventListener('click', removeCharsDuplicate);
sentence.addEventListener('keyup', () => {
    if (event.keyCode === 13) {
        execute.click();
    }
});

function removeCharsDuplicate() {
    let charsRemove = [];
    let sentenceValue = sentence.value;
    let words = splitViaSeparators(sentenceValue, separators);

    // Find duplicate symbols.
    words.forEach(
        word => {
            word.split('').forEach(
                (char, i) => {
                    if (word.indexOf(char, i + 1) !== -1) {
                        charsRemove.push(char);
                    }
                });
        });

    isDEBUG && console.log('Symbols to be removed from the sentence: ' + charsRemove + '.');

    // Exclude duplicate symbols.
    // join words -> split -> 
    // return symbols not included in charsRemove -> again join.
    sentenceValue = words.join(' ')
        .split('')
        .filter(char => {
            if (!charsRemove.includes(char)) {
                return char;
            }
        }).join('');

    result.value = sentenceValue;
}

function splitViaSeparators(str, separ) {

    for (let i = 0; i < str.length; i++) {
        if (separ.includes(str[i])) {
            str = str.replace(str[i], " ");
        }
    }

    return str.split(' ').filter(
        word => {
            if (word.length > 0) {
                return word;
            }
        }
    );
}