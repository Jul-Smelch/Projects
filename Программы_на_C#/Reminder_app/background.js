'use strict';

//Описываем что сработает, когда таймер закончится: с иконки снимаем текст 
//и показываем всплывающее уведомление о напоминании зарядки.
chrome.alarms.onAlarm.addListener (() => {
    chrome.action.setBadgeText({ text: '' })
    chrome.notifications.create({
        type: 'basic',
        iconUrl: 'icon2.png',        
        title: 'Напоминание',
        message: "Ты уже засиделся. Пора вставать и делать зарядку!",
        buttons: [{ title: 'Ок' }],
        priority: 0
    })
})

chrome.notifications.onButtonClicked.addListener(() =>{})
