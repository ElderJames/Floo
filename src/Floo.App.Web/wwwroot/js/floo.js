
export function SavePreFetchedData(key, data) {
  var dom = document.querySelector(`#${key}`);
  if (!dom) {
    dom = document.createElement('input');
    dom.type = "hidden";
    document.body.appendChild(dom);
  }
  dom.value = data;
}

export function GetPreFetchedData(key) {
  var dom = document.querySelector(`#${key}`);
  if (dom) {
    return dom.value;
  }
  console.log('returning ...')
  return "";
}


