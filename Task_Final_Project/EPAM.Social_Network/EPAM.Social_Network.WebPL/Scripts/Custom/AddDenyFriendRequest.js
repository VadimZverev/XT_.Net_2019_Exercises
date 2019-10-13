$(document).ready(function () {

    $(document).on('click', '.ajax-add', (e) => {
        e.preventDefault();

        let $parent = $(e.currentTarget).parent();
        let accId = $parent.children('input[name=accId]').val();
        let friendId = $parent.children('input[name=friendId]').val();

        $.ajax({
            type: "POST",
            url: "/Pages/AjaxPages/ConfirmFriendRequest",
            data: {
                accId: accId,
                friendId: friendId
            },
            success: (data) => {
                $parent.html(data);
            }
        });
    });

    $(document).on('click', '.ajax-deny', (e) => {
        e.preventDefault();

        let $parent = $(e.currentTarget).parent();
        let accId = $parent.children('input[name=accId]').val();
        let friendId = $parent.children('input[name=friendId]').val();

        $.ajax({
            type: "POST",
            url: "/Pages/AjaxPages/DenyFriendRequest",
            data: {
                accId: accId,
                friendId: friendId
            },
            success: (data) => {
                $parent.html(data);
            }
        });
    });
});