export const transDate = (date: string) => {
    let dateObj = new Date(date);
    var day = `${dateObj.getFullYear()}-${dateObj.getMonth() + 1}-${dateObj.getDate()}`;
    var time = dateObj.getHours() + ":" + dateObj.getMinutes();
    return day + " " + time;//date.replace("T"," ");
}