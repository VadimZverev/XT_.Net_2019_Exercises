$(document).ready(() => {
    $('.delete').click(() => {
        let idForm = $(event.currentTarget).parent();
        idForm = $(idForm).attr('id');
        $('.modal-footer button').attr('form', idForm);
        $('#exampleModal').modal('show');
    });
});