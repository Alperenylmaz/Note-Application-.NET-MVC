var modalCommentBodyId = "#model_comment_body";
var noteid = -1;

$(function () {
    $('#model_comment').on('show.bs.modal', function (e) {

        var btn = $(e.relatedTarget);
        noteid = btn.data("note-id");

        $(modalCommentBodyId).load("/Comment/ShowNoteComments/" + noteid);
    })
});

function doComment(btn, click, commentid, spanid) {

    var button = $(btn);
    var mode = button.data("edit-mode");

    if (click == "edit_click") {
        if (!mode) {
            button.data("edit-mode", true);
            button.removeClass("btn-warning");
            button.addClass("btn-success");

            //var btnSpan = btn.find("span");
            //btnSpan.removeClass("");
            //btnSpan.addClass("");
            $(spanid).addClass("editable");
            $(spanid).attr("contenteditable", true);
            $(spanid).focus();
        }
        else {
            button.data("edit-mode", false);
            button.addClass("btn-warning");
            button.removeClass("btn-success");
            //var btnSpan = btn.find("span");
            //btnSpan.removeClass("");
            //btnSpan.addClass("");
            $(spanid).removeClass("editable");
            $(spanid).attr("contenteditable", false);

            var txt = $(spanid).text();


            $.ajax({
                method: "POST",
                url: "/Comment/Edit/" + commentid,
                data: { text: txt }


            }).done(function (data) {

                if (data.result) {
                    $(modalCommentBodyId).load("/Comment/ShowNoteComments/" + noteid);
                }
                else {
                    alert("Yorum güncellenemedi.");
                }

            }).fail(function () {
                alert("Sunucu ile bağlantı kurulamadı.");
            });

        }

    }
    else if (click == "delete_click") {

        var dialog_res = confirm("Yorum Silinsin Mi?");

        if (!dialog_res) return false;

        $.ajax({
            method: "GET",
            url: "/Comment/Delete/" + commentid
        }).done(function (data) {

            if (data.result) {
                $(modalCommentBodyId).load("/Comment/ShowNoteComments/" + noteid);
            }
            else {
                alert("Yorum silinemedi.");
            }

        }).fail(function () {
            alert("Sunucu ile bağlantı kurulamadı.");
        });
    }
    else if (click == "new_click") {

        var txt = $("#new_comment_text").val();

        $.ajax({
            method: "POST",
            url: "/Comment/Create/",
            data: { text: txt, "noteid": noteid }
        }).done(function (data) {

            if (data.result) {
                $(modalCommentBodyId).load("/Comment/ShowNoteComments/" + noteid);
            }
            else {
                alert("Yorum eklenemedi.");
            }

        }).fail(function () {
            alert("Sunucu ile bağlantı kurulamadı.");
        });
    }

}