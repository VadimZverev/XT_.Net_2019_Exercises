function removeCharsDuplicate() {
    var charsRemove = [];
    var sentence = document.getElementById("sentence").value;
    var separators = ["\t", "?", "!", ":", ";", ",", "."];
    var words = splitViaSeparators(sentence, separators);

    words.forEach(
        word => {
            word.split('').forEach(
                (char, i) => {
                    if (word.indexOf(char, i + 1) !== -1) {
                        charsRemove.push(char);
                    }
                });
        });

    console.log('Symbols to be removed from the sentence: ' + charsRemove);

    sentence = words.join(' ')
        .split('')
        .filter(char => {
            if (!charsRemove.includes(char)) {
                return char;
            }
        }).join('');

    document.getElementById("result").value = sentence;
}

function splitViaSeparators(sentence, separators) {

    for (var i = 0; i < sentence.length; i++) {
        if (separators.includes(sentence[i])) {
            sentence = sentence.replace(sentence[i], " ");
        }
    }

    return sentence.split(' ').filter(
        word => {
            if (word.length > 0) {
                return word;
            }
        }
    );
}