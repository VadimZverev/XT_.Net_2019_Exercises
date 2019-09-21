$(document).ready(() => {
    var avatar = document.getElementById("avatar");
    var delImage = document.getElementById("delImage");
    var filePath = document.getElementById("customFile");
    var fileName = document.getElementById("fileName");
    const defaultSrc = avatar.src;

    filePath.addEventListener("change", (e) => {
        let reader = new FileReader();
        reader.onloadend = () => {
            avatar.src = reader.result;
        };

        let file = e.target.files[0];

        if (file) {
            fileName.innerText = file.name;
            reader.readAsDataURL(file);
            if (delImage) {
                delImage.setAttribute("disabled", '');
            }
        } else {
            fileName.innerText = "Choose image file";
            avatar.src = defaultSrc;
            if (delImage) {
                delImage.removeAttribute('disabled', '');
            }
        }
    });
});