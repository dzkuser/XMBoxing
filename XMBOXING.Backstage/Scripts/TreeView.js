

var tree;

//得到父节点
function getParent(treeSet, data) {
    tree = treeSet;
    $.each(data, function (index, ele) {

        var speace = 0;
        if (ele[treeSet.ParentID] == 0) {
            PushDate(ele, treeSet, speace,0 , true);
            getChilden(ele, data, treeSet, speace, ele[treeSet.NodeID]);
        }
    });

    deleteIcon();
     
}

//删除图标
function deleteIcon() {
    var span = $(".dzktree");
    $.each(span, function (index, ele) {
        console.log("ele:" + $(ele).find("span").text())
        if ($("." + $(ele).find("span").text()).length == 0) {
            $(ele).find("img").remove();
        }
    });
}

//得到子节点
function getChilden(parent, data, treeSet, speace, parentIDs) {
    speace += 10;
    $.each(data, function (index, ele) {
        if (ele[treeSet.ParentID] == parent[treeSet.NodeID]) {

            PushDate(ele, treeSet, speace, parentIDs, false);
            getChilden(ele, data, treeSet, speace, ele[treeSet.NodeID]);
        }
    });
}

//展示数据
function PushDate(data, treeSet, speace, parentIDs, isP) {
    var ele = $(treeSet.HtmlElement);
    ele.css("padding-left", speace + "px");
    ele.addClass("dzktree")
    appendCheckBox(data, ele, treeSet, parentIDs);
    
    appendIcon(ele,treeSet);

    ele.val($(data)[treeSet.ViewValue]);
    var text = $("<text>");
    text.text(data[treeSet.ViewFiled]);
    ele.append(text);

    var childen = $("<span>");
    childen.text(data[treeSet.NodeID]);
    childen.hide();
    ele.append(childen);

    ele.addClass(parentIDs + "");
    if (!isP) {
     
        ele.hide();
    }
    ele.click(eleClick);

    $("#" + treeSet.ParentElementID).append(ele);
}

function appendIcon(ele, treeSet) {
    if (treeSet.icon == null) {
        return;
    }
    var icon = $("<img>");
    icon.addClass(treeSet.icon.class);
    icon.attr("src", treeSet.icon.src);
    ele.append(icon);
}

//添加checkbox
function appendCheckBox(data, ele, treeSet, parentIDs) {
    if (treeSet.checkbox == null) {
        return;
    }
    var checkbox = $("<input type='checkbox'>");
    checkbox.val(data[treeSet.checkbox.value]);
    checkbox.addClass("dzktreecheck");
    checkbox.addClass("ck" + parentIDs);
    checkbox.click(checkBoxClick);
    ele.append(checkbox);
}

//元素的点击事件
function eleClick() {
    var spanValue = $(this).find("span").text();
    var icon = $(this).find("img");
    var $span = $("." + spanValue);
    if ($span.css("display") == "none") {
        $span.show();
        icon.attr("src", tree.icon.unfold);
    } else { 
        icon.attr("src", tree.icon.shrink);
        $span.hide();
        hidleEle($span);
    }
}


//隐藏子节点元素
function hidleEle(data) {

    $.each(data, function (index, ele) {
        var spanValue = $(ele).find("span").text();
        var $span = $("." + spanValue);
        $span.hide();
        if ($span.length != 0) {
            hidleEle($span);
        }
    });

}

//复选框点击事件
function checkBoxClick(e) {
    e.stopPropagation();

  //  var span = $(this).nextAll("span").text();
    var ck = $(this);
    
    if (tree.checkbox.relevance) {
        var isCheck = true;
        if (!ck.prop("checked")) {
            isCheck = false;
        }         
        checkboxRelevance(ck, isCheck);
    } 
}

//checkbox 选中关联
function checkboxRelevance(checkbox,isCheck) {
    var span = $(checkbox).nextAll("span").text();
    console.log(span);
    var checks = $(".ck"+ span);
    if (checks.length != 0) {
        $.each(checks, function (index, ele) {
            $(ele).prop("checked", isCheck);
            checkboxRelevance(ele, isCheck);
        });
    }
}


//得到checkbox的值
function GetCheckBoxValue() {

    var checkedboxValue = [];

    $.each($(".dzktreecheck:checked"), function (index,ele) {
        checkedboxValue.push($(ele).val());
    });
    return checkedboxValue;
}

//获得checkbox的权限
function GetCheckBoxPowerValue() {
    var checkedboxValue =[];
    $.each($(".dzktreecheck:checked"), function (index, ele) {
        var span= $(ele).parent().find("span").text();
        var classNameArray = $(ele).parent().prop("className");
        var parent= classNameArray[classNameArray.length-1];
      //  checkedboxValue[span] = $(ele).val();
        var power = {         
        }
        if (isNaN(parent)) {
            parent = 0;
        }
        console.log(isNaN(parent));
        power["ParentID"] = parent;
        power["PowerName"] = $(ele).val();
        power["NodeID"] = span;
        checkedboxValue.push(power);
    });
    return checkedboxValue;
}

//选中checkbox
function CheckedCheckbox(data) {

    $.each(data, function (index, ele) {
       console.log($("." + ele.ParentID + "[value='" + ele.PowerName + "']"));
       $(".ck" + ele.ParentID + "[value='" + ele.PowerName + "']").prop("checked",true);
      
    });

}