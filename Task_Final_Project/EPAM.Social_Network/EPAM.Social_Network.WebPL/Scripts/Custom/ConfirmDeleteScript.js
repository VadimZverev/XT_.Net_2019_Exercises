$(document).ready(() => {
    let idForm;

    $('.delete').click(() => {
        let $parent = $(event.currentTarget).parent();
        idForm = $($parent).attr('id');
        $('.modal-footer button').attr('form', idForm);
        $('#exampleModal').modal('show');
    });
});