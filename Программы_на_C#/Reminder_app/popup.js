'use strict';

//Функция включенного таймера, описываем что сработает при вкл.: 
//поверх иконки пишем "ON", указываем время таймера, взятое из значения кнопки, 
//сохраняем время в памяти и сворачиваем поп-ап. 
function startAlarm(time) {
    chrome.action.setBadgeText({ text: 'ON' });
    chrome.action.setBadgeBackgroundColor({color: 'red'});

    const minutes = parseFloat(time.target.value);
    chrome.alarms.create({ delayInMinutes: minutes });
    chrome.storage.sync.set({ minutes: minutes });
    window.close();
}
  
//Функция для сброса таймера, приводим приложение к исходному состоянию: 
//иконка без текста, нет запущенных таймеров и закрыт поп-ап.
function clearAlarm() {
    chrome.action.setBadgeText({ text: '' });
    chrome.alarms.clearAll();
    window.close();
}

//Находим в файле html элемент по ID - нашу кнопку и с помощью метода хрома при клике 
//запускаем функцию включения таймера с передачей значения, либо функцию сброса таймера.
document.getElementById('30_min').addEventListener('click', startAlarm);
document.getElementById('60_min').addEventListener('click', startAlarm);
document.getElementById('switch_off').addEventListener('click', clearAlarm);

