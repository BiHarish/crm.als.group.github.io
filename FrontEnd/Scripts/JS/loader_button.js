function loadingbuttononPage(myPageButtonId) {
    $(myPageButtonId).click(function () {
        $(this).button('loading').delay(500000).queue(function () {
            $(this).button('complete');
            $(this).dequeue();
        });
    });
}
