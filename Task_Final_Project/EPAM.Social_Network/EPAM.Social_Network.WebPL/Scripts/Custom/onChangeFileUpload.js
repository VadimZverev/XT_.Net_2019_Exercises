$(document).ready(() => {
    var avatar = document.getElementById("avatar");
    var delImg = document.getElementById("delImg");
    var delImgHidden = document.getElementById("delImgHidden");
    var filePath = document.getElementById("customFile");
    var resetImg = document.getElementById("resetImg");
    const defaultSrc = avatar.src;

    avatar.addEventListener('click', () => {
        filePath.click();
    });

    resetImg.addEventListener('click', () => {
        avatar.src = defaultSrc;
        delImgHidden.value = false;
    });

    delImg.addEventListener('click', () => {
        delImgHidden.value = true;
        avatar.src = "/Content/Images/noimage.png";
    });

    filePath.addEventListener("change", (e) => {
        let reader = new FileReader();

        reader.onloadend = () => {
            avatar.src = reader.result;
        };

        let file = e.target.files[0];

        if (file) {
            reader.readAsDataURL(file);

            if (delImgHidden.value === 'true') {
                delImgHidden.value = false;
            }
        }
    });
});