import{o as v}from"./DRmxXga8.js";import{c as f,w,h as p}from"./DxdRxZrz.js";import{d8 as s,l as g,D as E}from"./DFVm4NPh.js";function h(){return/iPhone/gi.test(window.navigator.platform)||/Mac/gi.test(window.navigator.platform)&&window.navigator.maxTouchPoints>0}function L(){return/Android/gi.test(window.navigator.userAgent)}function P(){return h()||L()}function c(r,u,o){f.isServer||s(i=>{document.addEventListener(r,u,o),i(()=>document.removeEventListener(r,u,o))})}function A(r,u,o){f.isServer||s(i=>{window.addEventListener(r,u,o),i(()=>window.removeEventListener(r,u,o))})}function x(r,u,o=E(()=>!0)){function i(e,a){if(!o.value||e.defaultPrevented)return;let t=a(e);if(t===null||!t.getRootNode().contains(t))return;let m=function l(n){return typeof n=="function"?l(n()):Array.isArray(n)||n instanceof Set?n:[n]}(r);for(let l of m){if(l===null)continue;let n=l instanceof HTMLElement?l:v(l);if(n!=null&&n.contains(t)||e.composed&&e.composedPath().includes(n))return}return!w(t,p.Loose)&&t.tabIndex!==-1&&e.preventDefault(),u(e,t)}let d=g(null);c("pointerdown",e=>{var a,t;o.value&&(d.value=((t=(a=e.composedPath)==null?void 0:a.call(e))==null?void 0:t[0])||e.target)},!0),c("mousedown",e=>{var a,t;o.value&&(d.value=((t=(a=e.composedPath)==null?void 0:a.call(e))==null?void 0:t[0])||e.target)},!0),c("click",e=>{P()||d.value&&(i(e,()=>d.value),d.value=null)},!0),c("touchend",e=>i(e,()=>e.target instanceof HTMLElement?e.target:null),!0),A("blur",e=>i(e,()=>window.document.activeElement instanceof HTMLIFrameElement?window.document.activeElement:null),!0)}export{A as a,P as n,h as t,x as w};
