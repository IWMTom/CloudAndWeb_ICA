var http = new XMLHttpRequest();
var coreUrl = "http://task2final.azurewebsites.net/api/";
var buUrl = coreUrl + "BusinessUnit";
var staffListUrl = coreUrl + "";
var staffDetailUrl = coreUrl+ "";


function start() {
    hideStaffList();
    http.onreadystatechange = getBuList;
    http.open("GET", buUrl);
    http.send();
}

function getBuList() {
    if (http.readyState == 4 && http.status == 200) {
        var buisinessUnits = JSON.parse(http.responseText);
        if (buisinessUnits != null) {
            displayItemsInBuList(buisinessUnits);
        } else {
            hideAll();
        }
    }
}

function displayItemsInBuList(arr) {
          
var table = document.getElementById("buList");

table.innerHTML = "";
   
    for (var i = 0; i < arr.length; i++) {     
        var row = table.insertRow(0);     
        var cell1 = row.insertCell(0);       
        var cell2 = row.insertCell(1);
        cell1.innerHTML = arr[i].title;       
        var id = arr[i].businessUnitCode;        
        cell2.innerHTML = "&nbsp&nbsp&nbsp&nbsp<a href='#'     id='" + id + "' " + " >List Staff</a>";       
        document.getElementById(id).onclick = getStaff;
    }
}

function getStaff(e) {

    var detailUrl = staffListUrl + e.target.id;
    http.onreadystatechange = requestDetail;
    http.open("GET", detailUrl);
    http.send();

}

function requestDetail() {
    if (http.readyState == 4 && http.status == 200) {
        var staffList = JSON.parse(http.responseText);
        if (staffList != null) {
            displayStaffList(staffList);
        } else {
            hideStaffList();
        }
    }
}

function displayStaffList(arr) {
    
    document.getElementById("staffListHeader").style.visibility = "visible";
    document.getElementById("Staffdetail").style.visibility = "hidden";
    var table = document.getElementById("staffList");
    table.style.visibility = "visible";
    if (arr != null)
    {
        table.innerHTML = "";
        for (var i = 0; i < arr.length; i++) {   
            var row = table.insertRow(0);   
            var cell1 = row.insertCell(0);
            var cell2 = row.insertCell(1);
            cell1.innerHTML = arr[i].fullName;
            var id = arr[i].staffCode;
            cell2.innerHTML = "&nbsp&nbsp&nbsp&nbsp<a href='#'     id='" + id + "' " + " >Staff Detail</a>";
            document.getElementById(id).onclick = getStaffDetails;
        }
    }
}

function getStaffDetails(e) {
  
}

function requestStaffDetail() {
  
}
        
function displayStaffDetail(staff) {   
    document.getElementById("Staffdetail").style.visibility = "visible";
}

function hideAll() {
    document.getElementById("buList").style.visibility = "hidden";
    hideStaffList();
}

function hideStaffList() {
    document.getElementById("staffListHeader").style.visibility = "hidden";
    document.getElementById("staffList").style.visibility = "hidden";
    document.getElementById("Staffdetail").style.visibility = "hidden";
            
}

function hideStaffDetail() {
    document.getElementById("Staffdetail").style.visibility = "hidden";
}

window.onload = start;
