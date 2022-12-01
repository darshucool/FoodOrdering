//html links for collapse and expand buttons
var expandDiv = '<div class=\"customexpand\"  onclick=\"JavaScript:HideMarkStatusList(this)\">&nbsp;</div>';
var collapseDiv = '<div class=\"customcollapse\"  onclick=\"JavaScript:HideMarkStatusList(this)\">&nbsp;</div>';


//set the current mark and status to reload it after new instance creation
function SetCurrentSelectedMark(markUid, status) {
    currentSelectedMark = markUid;
    currentStatus = status;
}

var searchValueArray = [];
var noOfCols;

function MarkSearch(searchTextBox, table) {

    try {

        var col = $(searchTextBox).attr('name');
        var value = $(searchTextBox).val();

        for (var i = 0; i < searchValueArray.length; i++) {

            var gridCoulumnDataElement = searchValueArray[i];

            if (gridCoulumnDataElement.Col == col) {
                searchValueArray[i].Val = value;
                break;
            }
        }

        var $rows = $(document.getElementById(table)).find('tbody tr');

        $rows.each(function (index) {

            if (index != -1) {

                $row = $(this);

                var found = false;

                for (var x = 0; x < noOfCols; x++) {

                    if ($row.find("td").eq(x).text().toUpperCase().indexOf(searchValueArray[x].Val.toUpperCase()) == 0) {
                        found = true;
                    }
                    else {
                        found = false;
                        break;
                    }
                }


                if ($row.attr("name") == "markParentRow") {

                    if (found) {

                        $row.show(500);

                    }
                    else {

                        var rowid = $row.attr("id");
                        var markid = rowid.replace("markParentRow", "");

                        var childRow = $("#markRow" + markid);

                        var childDiv = $("#markDiv" + markid);

                        //only if child list is already loaded hide it and reset the expand link
                        if ($(childRow).css('display') != 'none') {
                            var linkDiv = "<div class=\"customexpand\" onclick=\"JavaScript:ShowMarkStatusList(this,{1})\">&nbsp;</div>";

                            linkDiv = linkDiv.replace("{1}", markid);

                            $("#tdmark" + markid).html(linkDiv);

                            $('#markRow' + markid).hide();
                        }

                        $row.hide(500);

                    }
                }
            }
        });

    } catch (err) {
        alert("globalMethod " + err);
    }
}

function SetMarkFilterColumnCount(columnCount) {

    try {
        searchValueArray = [];
        for (var i = 0; i < columnCount; i++) {
            searchValueArray.push({ Col: i, Val: "" });
        }

        noOfCols = columnCount;
    }
    catch (err) {
        alert("createSearchValueArray " + err);
    }
}

function HideMarkStatusList(src, markuid) {
    var linkDiv = "<div class=\"customexpand\" onclick=\"JavaScript:ShowMarkStatusList(this,{1})\">&nbsp;</div>";

    linkDiv = linkDiv.replace("{1}", markuid);

    //reset the expand link
    $("#tdmark" + markuid).html(linkDiv);

    $('#markRow' + markuid).hide();
}

function ShowMarkStatusList(src, markuid) {
    var linkDiv = "<div class=\"customcollapse\" onclick=\"JavaScript:HideMarkStatusList(this,{1})\">&nbsp;</div>";

    linkDiv = linkDiv.replace("{1}", markuid);

    //reset the collapse link
    $("#tdmark" + markuid).html(linkDiv);

    $('#markRow' + markuid).show();
}

function ResetDivElement(src) {
    $('#' + src).html('');
}

function ReloadContents(uid, status) {
    $.ajax({
        type: "GET",
        url: "../MarkAssignment/ReloadContent",
        data: "uid=" + uid + "&status=" + status,
        success: function (msg) {
            $("#ContentTable").html(msg);
        },
        error: function () {
            serverError();
        }
    });
}