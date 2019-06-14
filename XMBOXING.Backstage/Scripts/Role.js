
//得到所有角色
function GetRoles() {
    $.ajax({
        url: 'http://172.16.31.250:6875/Power/GetRoles',
        method: 'POST',
        dataType: 'json',
        success: function (data) {
            var result = JSON.parse(data.Result);

            $.each(result, function (index,ele) {
                var tr = $("<tr>");

                var td = $("<td>");
                td.text(ele.ID);
                var tdName = $("<td>");
                tdName.text(ele.RoleName);
                var tdState = $("<td>");
                tdState.text(ele.RoleStatus>0?"禁用":"启用");

                tr.append(td);                            
                $("#x").append(tr);
               
            });

        }
    });

}

//删除角色批量
function DeleteRoleIDs() {

    var param = {
        astrRoleIDs: JSON.stringify([2,3])
    }

    $.ajax({
        url: 'http://172.16.31.250:6875/Power/DeleteRoleMore',
        method: 'POST',
        data: param,
        dataType: 'json',
        success: function (data) {           
            console.log(data);
        }
    });
}

//添加角色
function InsertRole() {
    var param = {
        "RoleName": "dzk"
    }
    $.ajax({
        url: 'http://172.16.31.250:6875/Power/InsertRole',
        method: 'POST',
        data: param,
        dataType: 'json',
        success: function (data) {
            console.log(data);
        }
    });

}


//修改角色
function UpdateRole() {

    var param = {
        "ID":4,
        "RoleName": "kk"
    }
    $.ajax({
        url: 'http://172.16.31.250:6875/Power/UpdateRole',
        method: 'POST',
        data: param,
        dataType: 'json',
        success: function (data) {
            console.log(data);
        }
    });
}

//角色授权视图数据
function AuthorizationData(treeSet) {
    $.ajax({
        url: 'http://localhost:61648/Power/GetMenuPower',
        method: 'POST',
        dataType: 'json',
        success: function (data) {
           // console.log(data);
            getParent(tree, JSON.parse(data.Result));
         
        }
    });

} 

//角色授权
function RoleAuthorization() {

    var param = {
        //角色ID (这个要改)
        aintRoleID: 4,
        //获得选中的权限 （这个参数不用你改，就固定这样）
        astrPowerName: JSON.stringify(GetCheckBoxPowerValue())
    }

    $.ajax({
        url: 'http://localhost:61648/Power/AuthorizationRole',
        method: 'post',
        data: param,
        dataType: 'json',
        success: function (data) {
            // console.log(data);
            getParent(tree, JSON.parse(data.Result));
        }
    });


}

//根据角色ID得到权限
function GetPowerByRoleID() {

    var param = {
        //角色ID集合 (这个要改)
        astrRoleIDs: JSON.stringify([4])
     
    }

    $.ajax({
        url: 'http://localhost:61648/Power/GetPowerByRoleID',
        method: 'post',
        data: param,
        dataType: 'json',
        success: function (data) {
            console.log(data);
       
        }
    });

}

//角色分配用户
function RoleAllocationUser() {

    var param = {
        "aintRoleID": 4,
        "astrAccountNames": JSON.stringify(['dzk','kkk'])
    }

    $.ajax({
        url: 'http://localhost:61648/Power/AllocationRole',
        method: 'post',
        data: param,
        dataType: 'json',
        success: function (data) {
            console.log(data);
        
        }
    });


}


function GetRoleByUser() {
    //可以不要参数，用户登录后会存在session
    var param = {
        "astrAccountName":"dzk"
    }

    $.ajax({
        url: 'http://localhost:61648/Power/GetRoleByUser',
        method: 'post',
        data: param,
        dataType: 'json',
        success: function (data) {
            console.log(data);

        }
    });
}



