$(document).ready(function () {

    $('#searchForm').submit((e) => {
        e.preventDefault();

        let $form = $(e.currentTarget);

        let url = $form.attr('action');
        let searchTxt = $('#search').val();
        let sort = $('#sort').val();
        let byDesc = $('#byDesc').is(':checked') ? $('#byDesc').val() : null;
        let male = $('#male').val();
        let city = $('#city').val();

        $.ajax({
            type: "POST",
            url: url,
            data: {
                searchTxt: searchTxt,
                sort: sort,
                byDesc: byDesc,
                male: male,
                city: city
            },
            success: (data) => {
                $('#people').html(data);
            }
        });
    });
});