/*
 * ATTENTION: The "eval" devtool has been used (maybe by default in mode: "development").
 * This devtool is neither made for production nor for readable output files.
 * It uses "eval()" calls to create a separate source file in the browser devtools.
 * If you are trying to read the output file, select a different devtool (https://webpack.js.org/configuration/devtool/)
 * or disable the default devtool with "devtool: false".
 * If you are looking for production-ready output files, see mode: "production" (https://webpack.js.org/configuration/mode/).
 */
/******/ (() => { // webpackBootstrap
/******/ 	var __webpack_modules__ = ({

/***/ "./js/rtl.js":
/*!*******************!*\
  !*** ./js/rtl.js ***!
  \*******************/
/***/ (() => {

eval("{(function () {\n    \"use strict\";\n    var body = document.body;\n    if (!body) {\n        return;\n    }\n    if ((body.getAttribute(\"dir\") || \"\").toLowerCase() !== \"rtl\") {\n        return;\n    }\n    document.documentElement.setAttribute(\"dir\", \"rtl\");\n    if (!body.classList.contains(\"rtl-body\")) {\n        body.classList.add(\"rtl-body\");\n    }\n    window.__IS_RTL__ = true;\n})();\n\n\n//# sourceURL=webpack://cowash-template/./js/rtl.js?\n}");

/***/ })

/******/ 	});
/************************************************************************/
/******/ 	
/******/ 	// startup
/******/ 	// Load entry module and return exports
/******/ 	// This entry module can't be inlined because the eval devtool is used.
/******/ 	var __webpack_exports__ = {};
/******/ 	__webpack_modules__["./js/rtl.js"]();
/******/ 	
/******/ })()
;