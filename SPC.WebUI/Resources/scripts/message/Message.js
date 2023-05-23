var cpLanguage = 'ko-KR';
var msg_lang = new Object();

msg_lang['ko-KR'] = new Object();
msg_lang['ko-KR']['PARTNER_NAME'] = '협력사';
msg_lang['ko-KR']['TITLE_NAME'] = '통계적공정관리';
msg_lang['ko-KR']['SYSTEM_ERROR'] = '시스템오류가 발생했습니다.';
msg_lang['ko-KR']['LOGIN_ID_SAVE'] = '아이디 저장';
msg_lang['ko-KR']['LOGIN_ID_EMPTY'] = '아이디를 입력해 주세요.';
msg_lang['ko-KR']['LOGIN_PASSWORD_EMPTY'] = '패스워드를 입력해 주세요.';
msg_lang['ko-KR']['COOKIE_EMPTY'] = '쿠키 정보가 없습니다\r\n다시 로그인해 주십시오.';

msg_lang['en-US'] = new Object();
msg_lang['en-US']['PARTNER_NAME'] = 'Partner';
msg_lang['en-US']['TITLE_NAME'] = 'Statistical Process Control';
msg_lang['en-US']['SYSTEM_ERROR'] = 'A system error has occurred.';
msg_lang['en-US']['LOGIN_ID_SAVE'] = 'Save the ID';
msg_lang['en-US']['LOGIN_ID_EMPTY'] = 'Please enter a user id.';
msg_lang['en-US']['LOGIN_PASSWORD_EMPTY'] = 'Please enter a user password.';
msg_lang['en-US']['COOKIE_EMPTY'] = 'We do not have cookies\r\nPlease log in again.';

msg_lang['zh-CN'] = new Object();
msg_lang['zh-CN']['PARTNER_NAME'] = 'Partner';
msg_lang['zh-CN']['TITLE_NAME'] = 'Statistical Process Control';
msg_lang['zh-CN']['SYSTEM_ERROR'] = 'A system error has occurred.';
msg_lang['zh-CN']['LOGIN_ID_SAVE'] = 'Save the ID';
msg_lang['zh-CN']['LOGIN_ID_EMPTY'] = 'Please enter a user id.';
msg_lang['zh-CN']['LOGIN_PASSWORD_EMPTY'] = 'Please enter a user password.';
msg_lang['zh-CN']['COOKIE_EMPTY'] = 'We do not have cookies\r\nPlease log in again.';

function fnSetLanguage(lang) {
    cpLanguage = lang;
}

function fnGetMessage(msgId) {
    return msg_lang[cpLanguage][msgId];
}