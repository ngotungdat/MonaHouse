function api_update(my_data,domainUrl,token)
{
    var dataRequest = JSON.stringify(my_data);
    $.ajax({
        url: domainUrl,
        data: dataRequest,
        type: 'PUT',
        dataType: 'JSON',
        headers: {
            'Authorization': 'Bearer ' + token,
            'Content-Type': 'application/json'
        },
        contentType: "application/json",
        success: function (data) {
            return data;
        },
        error: function (xhr, status, error) {
            console.log("Result Update: " + status + " " + error + " " + xhr.status + " " + xhr.statusText)
        }
    });
}
function api_insert(my_data,domainUrl,token)
{
    var dataRequest = JSON.stringify(my_data);
    $.ajax({
        url: domainUrl,
        data: dataRequest,
        type: 'POST',
        dataType: 'JSON',
        headers: {
            'Authorization': 'Bearer ' + token,
            'Content-Type': 'application/json'
        },
        contentType: "application/json",
        success: function (data) {
            return data;
        },
        error: function (xhr, status, error) {
            console.log("Result Insert: " + status + " " + error + " " + xhr.status + " " + xhr.statusText)
        }
    });
}
function api_get(domainUrl,token)
{
    $.ajax({
        type: 'GET',
        dataType: "json",
        url: domainUrl,
        headers: {
            'Authorization': 'Bearer ' + token,
            'Content-Type': 'application/json'
        },
        success: function (data, status, xhr) {
            return data;
        },
        error: function (xhr, status, error) {
            alert("Result: " + status + " " + error + " " + xhr.status + " " + xhr.statusText)
        }
    });
}