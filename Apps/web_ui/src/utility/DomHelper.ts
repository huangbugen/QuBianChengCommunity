/**
 * string类型转Dom类型
 * @param arg string类型的字符串
 * @returns Dom对象
 */
export const stringToDom = function (arg: string) {
    // 先创建一个div
    var objElement = document.createElement("div");
    // 把内容放到div中
    objElement.innerHTML = arg;
    // 返回div中所有子节点
    return objElement.childNodes;
}

export const getQuoteText = (arg: string, maxLine: number = 3) => {
    // 1. 将string类型转成Dom类型，这样有利于我们对其中元素的判断
    var dom = stringToDom(arg);
    var quotetext = "";
    let lineCount = 0;
    for (const element of dom) {
        if (element.textContent != null && element.textContent.trim() != "") {
            quotetext += element.textContent + "<br>";
            if (maxLine <= ++lineCount)
                break;
        }
    }
    return quotetext;
}