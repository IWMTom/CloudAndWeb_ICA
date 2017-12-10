var http = new XMLHttpRequest(); // Used to request data from a web server
var coreUrl = "http://localhost:65026/api/";
var buUrl = coreUrl + "BusinessUnit";
var staffListUrl = coreUrl + "Staff/BusinessUnit/";
var staffDetailUrl = coreUrl+ "Staff/";

function start()
{
    hideStaffList();
    http.onreadystatechange = getBuList; // Called whenever the status of the HTTP request changes
    http.open("GET", buUrl); // Initialises a GET request to the Business Unit API
    http.send(); // Sends the request to the server
}

function getBuList()
{
    if (http.readyState == 4 && http.status == 200) // Checks to see if the ready state is "done" and the HTTP status is 200 (OK)
    {
        var businessUnits = JSON.parse(http.responseText); // Parses the JSON response
        if (businessUnits != null)
        {
            displayItemsInBuList(businessUnits);
        }
        else
        {
            hideAll();
        }
    }
}

function displayItemsInBuList(arr)
{     
    var table = document.getElementById("buList"); // Gets the HTML element with ID of "buList"
    table.innerHTML = ""; // Empties the element
   
    for (var i = 0; i < arr.length; i++)
    {     
        var row = table.insertRow(0);     
        var cell1 = row.insertCell(0);       
        var cell2 = row.insertCell(1);
        cell1.innerHTML = arr[i].title;       
        var id = arr[i].businessUnitCode;        
        cell2.innerHTML = "&nbsp;&nbsp;&nbsp;&nbsp; <a href='#' id='" + id + "' " + ">List Staff</a>";       
        document.getElementById(id).onclick = getStaff; // Sets the 'onclick' function of the link to call getStaff()
    }
}

function getStaff(e)
{
    var detailUrl = staffListUrl + e.target.id;
    http.onreadystatechange = requestDetail;
    http.open("GET", detailUrl); // Initialises a GET request to the Business Unit Detail API
    http.send();
}

function requestDetail()
{
    if (http.readyState == 4 && http.status == 200)
    {
        var staffList = JSON.parse(http.responseText);
        if (staffList != null)
        {
            displayStaffList(staffList);
        }
        else
        {
            hideStaffList();
        }
    }
}

function displayStaffList(arr)
{ 
    document.getElementById("staffListHeader").style.visibility = "visible";
    document.getElementById("Staffdetail").style.visibility = "hidden";
    var table = document.getElementById("staffList");
    table.style.visibility = "visible";
    if (arr != null)
    {
        table.innerHTML = "";
        for (var i = 0; i < arr.length; i++)
        {   
            var row = table.insertRow(0);   
            var cell1 = row.insertCell(0);
            var cell2 = row.insertCell(1);
            cell1.innerHTML = arr[i].fullName;
            var id = arr[i].staffCode;
            cell2.innerHTML = "&nbsp;&nbsp;&nbsp;&nbsp; <a href='#' id='" + id + "' " + ">Staff Detail</a>";
            document.getElementById(id).onclick = getStaffDetails;
        }
    }
}

function getStaffDetails(e)
{
    var detailUrl = staffDetailUrl + e.target.id;
    http.onreadystatechange = requestStaffDetail;
    http.open("GET", detailUrl);
    http.send(); 
}

function requestStaffDetail()
{
    if (http.readyState == 4 && http.status == 200) {
        var staffDetail = JSON.parse(http.responseText);
        if (staffDetail != null) {
            displayStaffDetail(staffDetail);
        }
        else {
            hideStaffDetail();
        }
    } 
}
        
function displayStaffDetail(staff)
{   
    // This method formats the output of the staff detail and displays it in HTML

    document.getElementById("Staffdetail").style.visibility = "visible";
    document.getElementById("staffCode").innerHTML = "Staff Code: " + staff.staffCode;
    document.getElementById("staffFirstName").innerHTML = "First Name: " + staff.firstName;
    document.getElementById("staffMiddleName").innerHTML = "Middle Name: " + staff.middleName;
    document.getElementById("staffLastName").innerHTML = "Last Name: " + staff.lastName;
    document.getElementById("staffEmail").innerHTML = "Email Address: " + staff.emailAddress;
    document.getElementById("staffProfile").innerHTML = "Profile: " + staff.profile;
    document.getElementById("staffStartDate").innerHTML = "Start Date: " + staff.startDate;
    document.getElementById("staffDOB").innerHTML = "Date of Birth: " + staff.dob;
}

function hideAll()
{
    document.getElementById("buList").style.visibility = "hidden";
    hideStaffList();
}

function hideStaffList()
{
    document.getElementById("staffListHeader").style.visibility = "hidden";
    document.getElementById("staffList").style.visibility = "hidden";
    document.getElementById("Staffdetail").style.visibility = "hidden";         
}

function hideStaffDetail()
{
    document.getElementById("Staffdetail").style.visibility = "hidden";
}

window.onload = start;