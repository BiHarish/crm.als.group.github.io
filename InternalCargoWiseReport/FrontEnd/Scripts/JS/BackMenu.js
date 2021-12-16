function viewMenu() {
    $.ajax({
        url: '/FrontEnd/BackMenuHandler.ashx',
        method: 'get',
        dataType: 'json',
        success: function (data) {
            buildMenu($('#menu'), data);
        }
    });
}
function buildMenu(parent, items) {
    $.each(items, function () {
        var queryFunc = "";
        var li = "";
        if (this.BackMenuParentId == "" || this.BackMenuParentId == null || this.BackMenuParentId == "0") {
            if (this.BackMenuURL != "#") {
                li = $('<li><a href="' + this.BackMenuURL + '?lovelyindexing=' + this.BackMenuId + '"><i class="fa fa-lock"></i><span>' + this.BackMenuName + '</span></a></li>');
            }
            else {
                li = $('<li class="dropdown treeview"><a href="' + this.BackMenuURL + '"><i class="fa fa-files-o"></i><span>' + this.BackMenuName + '</span><span class="pull-right-container"><i class="fa fa-angle-left pull-right"></i></span></a></li>');
            }
        } else {
            li = $('<li><a href="' + this.BackMenuURL + '?lovelyindexing=' + this.BackMenuId + '"><i class="fa fa-circle-o text-yellow"></i><span>' + this.BackMenuName + '</span></a></li>');
        }
        li.appendTo(parent);
        if (this.BackMenuChild) {
            if (this.BackMenuChild.length > 0) {
                var ul = $('<ul class="treeview-menu"></ul>');
                ul.appendTo(li);
                buildMenu(ul, this.BackMenuChild);
            }
        }
    });
}
$("#menu").on("click", "ul li.dropdown", function (e) {
    $('ul li.dropdown').not(this).find('ul').hide();
    $(this).find('ul').slideToggle("slow");
});
