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
            const success = data.Success;
                const resultMessage = data.ResultMessage;
                const resultCode = data.ResultCode;
                if (resultCode == 401 || resultMessage == 'Unauthorized') {
                    Swal.fire({
                        title: 'Phiên làm việc đã hết hạn?',
                        text: "Vui lòng đăng nhập lại để tiếp tục!",
                        type: 'warning',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33',
                        confirmButtonText: 'Yes, Login!'
                    }).then((result) => {
                        if (result.value == true) {
                            window.location.href = '/login/login';
                        }
                    })
                }
                if (!success) {
                    Swal.fire(
                        'Error!',
                        resultMessage,
                        'error'
                    )
                }
                else {
                    Swal.fire(
                        'Thông báo!',
                        'Thêm dữ liệu thành công!',
                        'success'
                    );
                }   
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
                const success = data.Success;
                const resultMessage = data.ResultMessage;
                const resultCode = data.ResultCode;
                if (resultCode == 401 || resultMessage == 'Unauthorized') {
                    Swal.fire({
                        title: 'Phiên làm việc đã hết hạn?',
                        text: "Vui lòng đăng nhập lại để tiếp tục!",
                        type: 'warning',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33',
                        confirmButtonText: 'Yes, Login!'
                    }).then((result) => {
                        if (result.value == true) {
                            window.location.href = '/login/login';
                        }
                    })
                }
                if (!success) {
                    Swal.fire(
                        'Error!',
                        resultMessage,
                        'error'
                    )
                }
                else {
                    Swal.fire(
                        'Thông báo!',
                        'Thêm dữ liệu thành công!',
                        'success'
                    );
                }   
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
                const success = data.Success;
                const resultMessage = data.ResultMessage;
                const resultCode = data.ResultCode;
                if (resultCode == 401 || resultMessage == 'Unauthorized') {
                    Swal.fire({
                        title: 'Phiên làm việc đã hết hạn?',
                        text: "Vui lòng đăng nhập lại để tiếp tục!",
                        type: 'warning',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33',
                        confirmButtonText: 'Yes, Login!'
                    }).then((result) => {
                        if (result.value == true) {
                            window.location.href = '/login/login';
                        }
                    })
                }
                if (!success) {
                    Swal.fire(
                        'Error!',
                        resultMessage,
                        'error'
                    )
                }
            result = data;
        },
        error: function (xhr, status, error) {
            alert("Result: " + status + " " + error + " " + xhr.status + " " + xhr.statusText)
        }
    });
    return result;
}