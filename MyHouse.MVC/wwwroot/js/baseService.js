async function api_update(my_data,domainUrl,token)
{ 
    var result = {};
    var dataRequest = JSON.stringify(my_data);
    await $.ajax({
        url: domainUrl,
        data: dataRequest,
        type: 'PUT',
        dataType: 'JSON',
        headers: {
            'Authorization': 'Bearer ' + token,
            'Content-Type': 'application/json'
        },
        contentType: "application/json",
        success: await function (data) {
            result = data;
        },
        error: function (xhr, status, error) {
            console.log("Result Update: " + status + " " + error + " " + xhr.status + " " + xhr.statusText)
        }
    });
    return result;
}
async function api_insert(my_data,domainUrl,token)
{
    var result = {};
    var dataRequest = JSON.stringify(my_data);
    await $.ajax({
        url: domainUrl,
        data: dataRequest,
        type: 'POST',
        dataType: 'JSON',
        headers: {
            'Authorization': 'Bearer ' + token,
            'Content-Type': 'application/json'
        },
        contentType: "application/json",
        success: await function (data) {
            result = data;
        },
        error: function (xhr, status, error) {
            console.log("Result Insert: " + status + " " + error + " " + xhr.status + " " + xhr.statusText)
        }
    });
    return result;
}

async function api_get(domainUrl,token)
{
    let result = {};
    await $.ajax({
        type: 'GET',
        dataType: "json",
        url: domainUrl,
        headers: {
            'Authorization': 'Bearer ' + token,
            'Content-Type': 'application/json'
        },
        success: await function (data, status, xhr) {
            result = data;
        },
        error: function (xhr, status, error) {
            alert("Result: " + status + " " + error + " " + xhr.status + " " + xhr.statusText)
        }
    });
    return result;
}