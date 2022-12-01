var searchValueArray = [];
var noOfCols;


function CustomSearch(searchTextBox, table) {

    var rowcount = 0;
    try {
        
        var col = $(searchTextBox).attr('name');
        var value = $(searchTextBox).val();
        var targetTable = document.getElementById(table);
        //alert(targetTable.rows.length.toString());
        
        for (var i = 0; i < searchValueArray.length; i++) {

            var gridCoulumnDataElement = searchValueArray[i];

            if (gridCoulumnDataElement.Col == col) {
                searchValueArray[i].Val = value;
                break;
            }
        }
        
        for(var rowIndex = 0; rowIndex < targetTable.rows.length; rowIndex++) {
            var rowData = '';
          
            //Get column count from header row
            if (rowIndex == 0) {
                targetTableColCount = targetTable.rows.item(rowIndex).cells.length;
                continue; //do not execute further code for header row.
            }

            //Process data rows. (rowIndex >= 1)
            for (var colIndex = 0; colIndex < targetTableColCount; colIndex++) {

                if (targetTable.rows.item(rowIndex).style.display != 'none') {
                    if (colIndex == col) {
                        rowData += targetTable.rows.item(rowIndex).cells.item(colIndex).textContent;
                    }
                }
            }


            
            //If search term is not found in row data
            //then hide the row, else show
            if (rowData.toUpperCase().indexOf(value.toUpperCase()) == -1 && rowIndex != 1) {
                targetTable.rows.item(rowIndex).style.display = 'none';
               // $row.hide(500);
            }
            else {
                targetTable.rows.item(rowIndex).style.display = 'table-row';
                rowcount++;
               // $row.show(500);
            }
        }

      //  var $rows = $(document.getElementById(table)).find('tbody tr');

//        $rows.each(function (index) {

//            if (index != -1) {

//                $row = $(this);

//                var found = false;

//                for (var x = 0; x < noOfCols; x++) {
//                   
//                    if ($row.find("td").eq(x).text().toUpperCase().indexOf(searchValueArray[x].Val.toUpperCase()) == 0) {
//                        found = true;
//                    }
//                    else {
//                        found = false;
//                        break;
//                    }
//                }

//                if (found) {
//                    $row.show(500);
//                }
//                else { $row.hide(500); }
//            }
        //        });
       // alert(rowcount.toString());

    } catch (err) {
        alert("globalMethod " + err);
    }
}

function SetFilterColumnCount(columnCount) {

    try {

        searchValueArray = [];
        for (var i = 0; i < columnCount; i++) {
            searchValueArray.push({ Col: i, Val: "" });
        }

        noOfCols = columnCount;
    }
    catch(err) {
        alert("createSearchValueArray " + err);
    }
}