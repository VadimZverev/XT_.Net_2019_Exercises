$(document).ready(function () {

    $(document).on('click', '#AddFriendRequest', (e) => {
        e.preventDefault();

        let _friendId = $('#friendId').val();
        let _authAccId = $('#authAccId').val();

        $.ajax({
            type: "POST",
            url: "/Pages/AjaxPages/AddFriendRequest",
            data: {
                authAccId: _authAccId,
                friendId: _friendId
            },
            success: (data) => {
                $('#btnFriendContainer').html(data);
            }
        });
    });
});