$(document).ready(function () {

    $(document).on('click', '.msgs', (e) => {
        e.preventDefault();

        let url = $(e.currentTarget).attr('href');
        let $parent = $(e.currentTarget).parent();
        let accName = $parent.children('input[name="accName"]').val();
        let friendName = $parent.children('input[name="friendName"]').val();

        let friendId = $parent.children('input[name="friendId"]').val();
        $('#send').children('input[name="friendId"]').val(friendId);
        
        $.ajax({
            type: "POST",
            url: url,
            data: {
                accName: accName,
                friendName: friendName
            },
            success: (data) => {
                $('#messages').html(data);
                $('#inputMsg').removeClass('d-none');
            }
        });
    });

    $(document).on('submit', '.sendMsg', (e) => {
        e.preventDefault();

        let $form = $(e.currentTarget);

        let url = $form.attr('action');
        let accId = $form.children('input[name="accId"]').val();
        let friendId = $form.children('input[name="friendId"]').val();
        let $sendTxt = $form.children('textarea[name="sendTxt"]');
        let sendTxtValue = $sendTxt.val();

        $.ajax({
            type: "POST",
            url: url,
            data: {
                accId: accId,
                friendId: friendId,
                sendTxt: sendTxtValue
            },
            success: (data) => {
                let $messages = $('#messages');
                $messages.append(data);
                $sendTxt.val('');
            }
        });
    });
});