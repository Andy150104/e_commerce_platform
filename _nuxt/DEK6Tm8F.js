const __vite__mapDeps=(i,m=__vite__mapDeps,d=(m.f||(m.f=["./BoNxg3xf.js","./DFVm4NPh.js","./entry.D6k2if_i.css","./PLEq0hNF.js","./0qAV6zC5.js","./DlAUqK2U.js","./B33bflHF.js","./DxTMCm_i.js","./CzO2N3BU.js","./BpbWEv0s.js","./-mdl1zzC.js","./B-rOowS4.js"])))=>i.map(i=>d[i]);
import{N as ee,Z as M,$,a0 as te,a1 as ie,a2 as re,a3 as B,a4 as z,a5 as ae,a6 as se,a7 as h,a8 as x,a9 as H,aa as de,ab as ce,ac as pe,_ as ue,o as L,c as I,a as r,O as qe,P as c,ad as me,ae as be,e as oe,l as le,af as F,b as u,w as T,G as S,i as p,q as fe,Y as C,y as E,x as P,C as U,ag as R,z as he,s as ge,f as ve,g as we,h as ke,I as j,L as ye,ah as xe}from"./DFVm4NPh.js";import{B as $e,u as O}from"./B05HPIib.js";import{Z as N}from"./BpXJ3t4k.js";import{C as _e}from"./CNgG3cJA.js";import{s as Se}from"./DPWrjAok.js";import{_ as Ce}from"./Bzy3NznM.js";import{_ as Ee}from"./BgQU1iFT.js";import{_ as W}from"./DTtOshDf.js";import{U as g}from"./COKDO915.js";import{u as V,_ as Q}from"./CwrryA99.js";import{_ as Oe}from"./D0GLTrGt.js";import{a as G}from"./DfFDNbgx.js";import{X as w}from"./BDEeDIwr.js";import"./D5y8FroI.js";import"./BuMp5kCj.js";import"./DqbQgBaR.js";/* empty css        */import"./uhLm67I1.js";import"./D8p04lkY.js";import"./DlAUqK2U.js";import"./V8n92ecy.js";import"./DPxmNygB.js";import"./DsIA2G5F.js";import"./BKCMpDB6.js";import"./PLEq0hNF.js";import"./0qAV6zC5.js";import"./B33bflHF.js";import"./DxTMCm_i.js";import"./DgmwlLno.js";import"./Bug8ffv2.js";import"./awSMyq0J.js";import"./Bj-PnIja.js";import"./B_7wH41M.js";import"./QbBb9rFs.js";import"./B1VAvD7b.js";import"./5Q-RKaCz.js";import"./BCNAepu4.js";var Te=({dt:t})=>`
.p-tooltip {
    position: absolute;
    display: none;
    max-width: ${t("tooltip.max.width")};
}

.p-tooltip-right,
.p-tooltip-left {
    padding: 0 ${t("tooltip.gutter")};
}

.p-tooltip-top,
.p-tooltip-bottom {
    padding: ${t("tooltip.gutter")} 0;
}

.p-tooltip-text {
    white-space: pre-line;
    word-break: break-word;
    background: ${t("tooltip.background")};
    color: ${t("tooltip.color")};
    padding: ${t("tooltip.padding")};
    box-shadow: ${t("tooltip.shadow")};
    border-radius: ${t("tooltip.border.radius")};
}

.p-tooltip-arrow {
    position: absolute;
    width: 0;
    height: 0;
    border-color: transparent;
    border-style: solid;
}

.p-tooltip-right .p-tooltip-arrow {
    margin-top: calc(-1 * ${t("tooltip.gutter")});
    border-width: ${t("tooltip.gutter")} ${t("tooltip.gutter")} ${t("tooltip.gutter")} 0;
    border-right-color: ${t("tooltip.background")};
}

.p-tooltip-left .p-tooltip-arrow {
    margin-top: calc(-1 * ${t("tooltip.gutter")});
    border-width: ${t("tooltip.gutter")} 0 ${t("tooltip.gutter")} ${t("tooltip.gutter")};
    border-left-color: ${t("tooltip.background")};
}

.p-tooltip-top .p-tooltip-arrow {
    margin-left: calc(-1 * ${t("tooltip.gutter")});
    border-width: ${t("tooltip.gutter")} ${t("tooltip.gutter")} 0 ${t("tooltip.gutter")};
    border-top-color: ${t("tooltip.background")};
    border-bottom-color: ${t("tooltip.background")};
}

.p-tooltip-bottom .p-tooltip-arrow {
    margin-left: calc(-1 * ${t("tooltip.gutter")});
    border-width: 0 ${t("tooltip.gutter")} ${t("tooltip.gutter")} ${t("tooltip.gutter")};
    border-top-color: ${t("tooltip.background")};
    border-bottom-color: ${t("tooltip.background")};
}
`,Le={root:"p-tooltip p-component",arrow:"p-tooltip-arrow",text:"p-tooltip-text"},Ae=ee.extend({name:"tooltip-directive",style:Te,classes:Le}),De=$e.extend({style:Ae});function ze(t,e){return Ve(t)||Ie(t,e)||Pe(t,e)||He()}function He(){throw new TypeError(`Invalid attempt to destructure non-iterable instance.
In order to be iterable, non-array objects must have a [Symbol.iterator]() method.`)}function Pe(t,e){if(t){if(typeof t=="string")return K(t,e);var o={}.toString.call(t).slice(8,-1);return o==="Object"&&t.constructor&&(o=t.constructor.name),o==="Map"||o==="Set"?Array.from(t):o==="Arguments"||/^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(o)?K(t,e):void 0}}function K(t,e){(e==null||e>t.length)&&(e=t.length);for(var o=0,l=Array(e);o<e;o++)l[o]=t[o];return l}function Ie(t,e){var o=t==null?null:typeof Symbol<"u"&&t[Symbol.iterator]||t["@@iterator"];if(o!=null){var l,n,i,d,s=[],a=!0,q=!1;try{if(i=(o=o.call(t)).next,e!==0)for(;!(a=(l=i.call(o)).done)&&(s.push(l.value),s.length!==e);a=!0);}catch(b){q=!0,n=b}finally{try{if(!a&&o.return!=null&&(d=o.return(),Object(d)!==d))return}finally{if(q)throw n}}return s}}function Ve(t){if(Array.isArray(t))return t}function Z(t,e,o){return(e=Me(e))in t?Object.defineProperty(t,e,{value:o,enumerable:!0,configurable:!0,writable:!0}):t[e]=o,t}function Me(t){var e=Be(t,"string");return k(e)=="symbol"?e:e+""}function Be(t,e){if(k(t)!="object"||!t)return t;var o=t[Symbol.toPrimitive];if(o!==void 0){var l=o.call(t,e);if(k(l)!="object")return l;throw new TypeError("@@toPrimitive must return a primitive value.")}return(e==="string"?String:Number)(t)}function k(t){"@babel/helpers - typeof";return k=typeof Symbol=="function"&&typeof Symbol.iterator=="symbol"?function(e){return typeof e}:function(e){return e&&typeof Symbol=="function"&&e.constructor===Symbol&&e!==Symbol.prototype?"symbol":typeof e},k(t)}var Fe=De.extend("tooltip",{beforeMount:function(e,o){var l,n=this.getTarget(e);if(n.$_ptooltipModifiers=this.getModifiers(o),o.value){if(typeof o.value=="string")n.$_ptooltipValue=o.value,n.$_ptooltipDisabled=!1,n.$_ptooltipEscape=!0,n.$_ptooltipClass=null,n.$_ptooltipFitContent=!0,n.$_ptooltipIdAttr=O("pv_id")+"_tooltip",n.$_ptooltipShowDelay=0,n.$_ptooltipHideDelay=0,n.$_ptooltipAutoHide=!0;else if(k(o.value)==="object"&&o.value){if(M(o.value.value)||o.value.value.trim()==="")return;n.$_ptooltipValue=o.value.value,n.$_ptooltipDisabled=!!o.value.disabled===o.value.disabled?o.value.disabled:!1,n.$_ptooltipEscape=!!o.value.escape===o.value.escape?o.value.escape:!0,n.$_ptooltipClass=o.value.class||"",n.$_ptooltipFitContent=!!o.value.fitContent===o.value.fitContent?o.value.fitContent:!0,n.$_ptooltipIdAttr=o.value.id||O("pv_id")+"_tooltip",n.$_ptooltipShowDelay=o.value.showDelay||0,n.$_ptooltipHideDelay=o.value.hideDelay||0,n.$_ptooltipAutoHide=!!o.value.autoHide===o.value.autoHide?o.value.autoHide:!0}}else return;n.$_ptooltipZIndex=(l=o.instance.$primevue)===null||l===void 0||(l=l.config)===null||l===void 0||(l=l.zIndex)===null||l===void 0?void 0:l.tooltip,this.bindEvents(n,o),e.setAttribute("data-pd-tooltip",!0)},updated:function(e,o){var l=this.getTarget(e);if(l.$_ptooltipModifiers=this.getModifiers(o),this.unbindEvents(l),!!o.value){if(typeof o.value=="string")l.$_ptooltipValue=o.value,l.$_ptooltipDisabled=!1,l.$_ptooltipEscape=!0,l.$_ptooltipClass=null,l.$_ptooltipIdAttr=l.$_ptooltipIdAttr||O("pv_id")+"_tooltip",l.$_ptooltipShowDelay=0,l.$_ptooltipHideDelay=0,l.$_ptooltipAutoHide=!0,this.bindEvents(l,o);else if(k(o.value)==="object"&&o.value)if(M(o.value.value)||o.value.value.trim()===""){this.unbindEvents(l,o);return}else l.$_ptooltipValue=o.value.value,l.$_ptooltipDisabled=!!o.value.disabled===o.value.disabled?o.value.disabled:!1,l.$_ptooltipEscape=!!o.value.escape===o.value.escape?o.value.escape:!0,l.$_ptooltipClass=o.value.class||"",l.$_ptooltipFitContent=!!o.value.fitContent===o.value.fitContent?o.value.fitContent:!0,l.$_ptooltipIdAttr=o.value.id||l.$_ptooltipIdAttr||O("pv_id")+"_tooltip",l.$_ptooltipShowDelay=o.value.showDelay||0,l.$_ptooltipHideDelay=o.value.hideDelay||0,l.$_ptooltipAutoHide=!!o.value.autoHide===o.value.autoHide?o.value.autoHide:!0,this.bindEvents(l,o)}},unmounted:function(e,o){var l=this.getTarget(e);this.remove(l),this.unbindEvents(l,o),l.$_ptooltipScrollHandler&&(l.$_ptooltipScrollHandler.destroy(),l.$_ptooltipScrollHandler=null)},timer:void 0,methods:{bindEvents:function(e,o){var l=this,n=e.$_ptooltipModifiers;n.focus?(e.$_focusevent=function(i){return l.onFocus(i,o)},e.addEventListener("focus",e.$_focusevent),e.addEventListener("blur",this.onBlur.bind(this))):(e.$_mouseenterevent=function(i){return l.onMouseEnter(i,o)},e.addEventListener("mouseenter",e.$_mouseenterevent),e.addEventListener("mouseleave",this.onMouseLeave.bind(this)),e.addEventListener("click",this.onClick.bind(this))),e.addEventListener("keydown",this.onKeydown.bind(this))},unbindEvents:function(e){var o=e.$_ptooltipModifiers;o.focus?(e.removeEventListener("focus",e.$_focusevent),e.$_focusevent=null,e.removeEventListener("blur",this.onBlur.bind(this))):(e.removeEventListener("mouseenter",e.$_mouseenterevent),e.$_mouseenterevent=null,e.removeEventListener("mouseleave",this.onMouseLeave.bind(this)),e.removeEventListener("click",this.onClick.bind(this))),e.removeEventListener("keydown",this.onKeydown.bind(this))},bindScrollListener:function(e){var o=this;e.$_ptooltipScrollHandler||(e.$_ptooltipScrollHandler=new _e(e,function(){o.hide(e)})),e.$_ptooltipScrollHandler.bindScrollListener()},unbindScrollListener:function(e){e.$_ptooltipScrollHandler&&e.$_ptooltipScrollHandler.unbindScrollListener()},onMouseEnter:function(e,o){var l=e.currentTarget,n=l.$_ptooltipShowDelay;this.show(l,o,n)},onMouseLeave:function(e){var o=e.currentTarget,l=o.$_ptooltipHideDelay,n=o.$_ptooltipAutoHide;if(n)this.hide(o,l);else{var i=$(e.target,"data-pc-name")==="tooltip"||$(e.target,"data-pc-section")==="arrow"||$(e.target,"data-pc-section")==="text"||$(e.relatedTarget,"data-pc-name")==="tooltip"||$(e.relatedTarget,"data-pc-section")==="arrow"||$(e.relatedTarget,"data-pc-section")==="text";!i&&this.hide(o,l)}},onFocus:function(e,o){var l=e.currentTarget,n=l.$_ptooltipShowDelay;this.show(l,o,n)},onBlur:function(e){var o=e.currentTarget,l=o.$_ptooltipHideDelay;this.hide(o,l)},onClick:function(e){var o=e.currentTarget,l=o.$_ptooltipHideDelay;this.hide(o,l)},onKeydown:function(e){var o=e.currentTarget,l=o.$_ptooltipHideDelay;e.code==="Escape"&&this.hide(e.currentTarget,l)},tooltipActions:function(e,o){if(!(e.$_ptooltipDisabled||!te(e))){var l=this.create(e,o);this.align(e),!this.isUnstyled()&&ie(l,250);var n=this;window.addEventListener("resize",function i(){re()||n.hide(e),window.removeEventListener("resize",i)}),l.addEventListener("mouseleave",function i(){n.hide(e),l.removeEventListener("mouseleave",i),e.removeEventListener("mouseenter",e.$_mouseenterevent),setTimeout(function(){return e.addEventListener("mouseenter",e.$_mouseenterevent)},50)}),this.bindScrollListener(e),N.set("tooltip",l,e.$_ptooltipZIndex)}},show:function(e,o,l){var n=this;l!==void 0?this.timer=setTimeout(function(){return n.tooltipActions(e,o)},l):this.tooltipActions(e,o)},tooltipRemoval:function(e){this.remove(e),this.unbindScrollListener(e)},hide:function(e,o){var l=this;clearTimeout(this.timer),o!==void 0?setTimeout(function(){return l.tooltipRemoval(e)},o):this.tooltipRemoval(e)},getTooltipElement:function(e){return document.getElementById(e.$_ptooltipId)},getArrowElement:function(e){var o=this.getTooltipElement(e);return B(o,'[data-pc-section="arrow"]')},create:function(e){var o=e.$_ptooltipModifiers,l=z("div",{class:!this.isUnstyled()&&this.cx("arrow"),"p-bind":this.ptm("arrow",{context:o})}),n=z("div",{class:!this.isUnstyled()&&this.cx("text"),"p-bind":this.ptm("text",{context:o})});e.$_ptooltipEscape?(n.innerHTML="",n.appendChild(document.createTextNode(e.$_ptooltipValue))):n.innerHTML=e.$_ptooltipValue;var i=z("div",Z(Z({id:e.$_ptooltipIdAttr,role:"tooltip",style:{display:"inline-block",width:e.$_ptooltipFitContent?"fit-content":void 0,pointerEvents:!this.isUnstyled()&&e.$_ptooltipAutoHide&&"none"},class:[!this.isUnstyled()&&this.cx("root"),e.$_ptooltipClass]},this.$attrSelector,""),"p-bind",this.ptm("root",{context:o})),l,n);return document.body.appendChild(i),e.$_ptooltipId=i.id,this.$el=i,i},remove:function(e){if(e){var o=this.getTooltipElement(e);o&&o.parentElement&&(N.clear(o),document.body.removeChild(o)),e.$_ptooltipId=null}},align:function(e){var o=e.$_ptooltipModifiers;o.top?(this.alignTop(e),this.isOutOfBounds(e)&&(this.alignBottom(e),this.isOutOfBounds(e)&&this.alignTop(e))):o.left?(this.alignLeft(e),this.isOutOfBounds(e)&&(this.alignRight(e),this.isOutOfBounds(e)&&(this.alignTop(e),this.isOutOfBounds(e)&&(this.alignBottom(e),this.isOutOfBounds(e)&&this.alignLeft(e))))):o.bottom?(this.alignBottom(e),this.isOutOfBounds(e)&&(this.alignTop(e),this.isOutOfBounds(e)&&this.alignBottom(e))):(this.alignRight(e),this.isOutOfBounds(e)&&(this.alignLeft(e),this.isOutOfBounds(e)&&(this.alignTop(e),this.isOutOfBounds(e)&&(this.alignBottom(e),this.isOutOfBounds(e)&&this.alignRight(e)))))},getHostOffset:function(e){var o=e.getBoundingClientRect(),l=o.left+ae(),n=o.top+se();return{left:l,top:n}},alignRight:function(e){this.preAlign(e,"right");var o=this.getTooltipElement(e),l=this.getArrowElement(e),n=this.getHostOffset(e),i=n.left+h(e),d=n.top+(x(e)-x(o))/2;o.style.left=i+"px",o.style.top=d+"px",l.style.top="50%",l.style.right=null,l.style.bottom=null,l.style.left="0"},alignLeft:function(e){this.preAlign(e,"left");var o=this.getTooltipElement(e),l=this.getArrowElement(e),n=this.getHostOffset(e),i=n.left-h(o),d=n.top+(x(e)-x(o))/2;o.style.left=i+"px",o.style.top=d+"px",l.style.top="50%",l.style.right="0",l.style.bottom=null,l.style.left=null},alignTop:function(e){this.preAlign(e,"top");var o=this.getTooltipElement(e),l=this.getArrowElement(e),n=h(o),i=h(e),d=H(),s=d.width,a=this.getHostOffset(e),q=a.left+(h(e)-h(o))/2,b=a.top-x(o);a.left<n/2&&(q=a.left),a.left+n>s&&(q=Math.floor(a.left+i-n)),o.style.left=q+"px",o.style.top=b+"px";var y=a.left-this.getHostOffset(o).left+i/2;l.style.top=null,l.style.right=null,l.style.bottom="0",l.style.left=y+"px"},alignBottom:function(e){this.preAlign(e,"bottom");var o=this.getTooltipElement(e),l=this.getArrowElement(e),n=h(o),i=h(e),d=H(),s=d.width,a=this.getHostOffset(e),q=a.left+(h(e)-h(o))/2,b=a.top+x(e);a.left<n/2&&(q=a.left),a.left+n>s&&(q=Math.floor(a.left+i-n)),o.style.left=q+"px",o.style.top=b+"px";var y=a.left-this.getHostOffset(o).left+i/2;l.style.top="0",l.style.right=null,l.style.bottom=null,l.style.left=y+"px"},preAlign:function(e,o){var l=this.getTooltipElement(e);l.style.left="-999px",l.style.top="-999px",de(l,"p-tooltip-".concat(l.$_ptooltipPosition)),!this.isUnstyled()&&ce(l,"p-tooltip-".concat(o)),l.$_ptooltipPosition=o,l.setAttribute("data-p-position",o)},isOutOfBounds:function(e){var o=this.getTooltipElement(e),l=o.getBoundingClientRect(),n=l.top,i=l.left,d=h(o),s=x(o),a=H();return i+d>a.width||i<0||n<0||n+s>a.height},getTarget:function(e){var o;return pe(e,"p-inputwrapper")&&(o=B(e,"input"))!==null&&o!==void 0?o:e},getModifiers:function(e){return e.modifiers&&Object.keys(e.modifiers).length?e.modifiers:e.arg&&k(e.arg)==="object"?Object.entries(e.arg).reduce(function(o,l){var n=ze(l,2),i=n[0],d=n[1];return(i==="event"||i==="position")&&(o[d]=!0),o},{}):{}}}}),Ue=({dt:t})=>`
/*!
* Quill Editor v1.3.3
* https://quilljs.com/
* Copyright (c) 2014, Jason Chen
* Copyright (c) 2013, salesforce.com
*/
.ql-container {
    box-sizing: border-box;
    font-family: Helvetica, Arial, sans-serif;
    font-size: 13px;
    height: 100%;
    margin: 0;
    position: relative;
}
.ql-container.ql-disabled .ql-tooltip {
    visibility: hidden;
}
.ql-container.ql-disabled .ql-editor ul[data-checked] > li::before {
    pointer-events: none;
}
.ql-clipboard {
    inset-inline-start: -100000px;
    height: 1px;
    overflow-y: hidden;
    position: absolute;
    top: 50%;
}
.ql-clipboard p {
    margin: 0;
    padding: 0;
}
.ql-editor {
    box-sizing: border-box;
    line-height: 1.42;
    height: 100%;
    outline: none;
    overflow-y: auto;
    padding: 12px 15px;
    tab-size: 4;
    -moz-tab-size: 4;
    text-align: left;
    white-space: pre-wrap;
    word-wrap: break-word;
}
.ql-editor > * {
    cursor: text;
}
.ql-editor p,
.ql-editor ol,
.ql-editor ul,
.ql-editor pre,
.ql-editor blockquote,
.ql-editor h1,
.ql-editor h2,
.ql-editor h3,
.ql-editor h4,
.ql-editor h5,
.ql-editor h6 {
    margin: 0;
    padding: 0;
    counter-reset: list-1 list-2 list-3 list-4 list-5 list-6 list-7 list-8 list-9;
}
.ql-editor ol,
.ql-editor ul {
    padding-inline-start: 1.5rem;
}
.ql-editor ol > li,
.ql-editor ul > li {
    list-style-type: none;
}
.ql-editor ul > li::before {
    content: '\\2022';
}
.ql-editor ul[data-checked='true'],
.ql-editor ul[data-checked='false'] {
    pointer-events: none;
}
.ql-editor ul[data-checked='true'] > li *,
.ql-editor ul[data-checked='false'] > li * {
    pointer-events: all;
}
.ql-editor ul[data-checked='true'] > li::before,
.ql-editor ul[data-checked='false'] > li::before {
    color: #777;
    cursor: pointer;
    pointer-events: all;
}
.ql-editor ul[data-checked='true'] > li::before {
    content: '\\2611';
}
.ql-editor ul[data-checked='false'] > li::before {
    content: '\\2610';
}
.ql-editor li::before {
    display: inline-block;
    white-space: nowrap;
    width: 1.2rem;
}
.ql-editor li:not(.ql-direction-rtl)::before {
    margin-inline-start: -1.5rem;
    margin-inline-end: 0.3rem;
    text-align: right;
}
.ql-editor li.ql-direction-rtl::before {
    margin-inline-start: 0.3rem;
    margin-inline-end: -1.5rem;
}
.ql-editor ol li:not(.ql-direction-rtl),
.ql-editor ul li:not(.ql-direction-rtl) {
    padding-inline-start: 1.5rem;
}
.ql-editor ol li.ql-direction-rtl,
.ql-editor ul li.ql-direction-rtl {
    padding-inline-end: 1.5rem;
}
.ql-editor ol li {
    counter-reset: list-1 list-2 list-3 list-4 list-5 list-6 list-7 list-8 list-9;
    counter-increment: list-0;
}
.ql-editor ol li:before {
    content: counter(list-0, decimal) '. ';
}
.ql-editor ol li.ql-indent-1 {
    counter-increment: list-1;
}
.ql-editor ol li.ql-indent-1:before {
    content: counter(list-1, lower-alpha) '. ';
}
.ql-editor ol li.ql-indent-1 {
    counter-reset: list-2 list-3 list-4 list-5 list-6 list-7 list-8 list-9;
}
.ql-editor ol li.ql-indent-2 {
    counter-increment: list-2;
}
.ql-editor ol li.ql-indent-2:before {
    content: counter(list-2, lower-roman) '. ';
}
.ql-editor ol li.ql-indent-2 {
    counter-reset: list-3 list-4 list-5 list-6 list-7 list-8 list-9;
}
.ql-editor ol li.ql-indent-3 {
    counter-increment: list-3;
}
.ql-editor ol li.ql-indent-3:before {
    content: counter(list-3, decimal) '. ';
}
.ql-editor ol li.ql-indent-3 {
    counter-reset: list-4 list-5 list-6 list-7 list-8 list-9;
}
.ql-editor ol li.ql-indent-4 {
    counter-increment: list-4;
}
.ql-editor ol li.ql-indent-4:before {
    content: counter(list-4, lower-alpha) '. ';
}
.ql-editor ol li.ql-indent-4 {
    counter-reset: list-5 list-6 list-7 list-8 list-9;
}
.ql-editor ol li.ql-indent-5 {
    counter-increment: list-5;
}
.ql-editor ol li.ql-indent-5:before {
    content: counter(list-5, lower-roman) '. ';
}
.ql-editor ol li.ql-indent-5 {
    counter-reset: list-6 list-7 list-8 list-9;
}
.ql-editor ol li.ql-indent-6 {
    counter-increment: list-6;
}
.ql-editor ol li.ql-indent-6:before {
    content: counter(list-6, decimal) '. ';
}
.ql-editor ol li.ql-indent-6 {
    counter-reset: list-7 list-8 list-9;
}
.ql-editor ol li.ql-indent-7 {
    counter-increment: list-7;
}
.ql-editor ol li.ql-indent-7:before {
    content: counter(list-7, lower-alpha) '. ';
}
.ql-editor ol li.ql-indent-7 {
    counter-reset: list-8 list-9;
}
.ql-editor ol li.ql-indent-8 {
    counter-increment: list-8;
}
.ql-editor ol li.ql-indent-8:before {
    content: counter(list-8, lower-roman) '. ';
}
.ql-editor ol li.ql-indent-8 {
    counter-reset: list-9;
}
.ql-editor ol li.ql-indent-9 {
    counter-increment: list-9;
}
.ql-editor ol li.ql-indent-9:before {
    content: counter(list-9, decimal) '. ';
}
.ql-editor .ql-video {
    display: block;
    max-width: 100%;
}
.ql-editor .ql-video.ql-align-center {
    margin: 0 auto;
}
.ql-editor .ql-video.ql-align-right {
    margin: 0 0 0 auto;
}
.ql-editor .ql-bg-black {
    background: #000;
}
.ql-editor .ql-bg-red {
    background: #e60000;
}
.ql-editor .ql-bg-orange {
    background: #f90;
}
.ql-editor .ql-bg-yellow {
    background: #ff0;
}
.ql-editor .ql-bg-green {
    background: #008a00;
}
.ql-editor .ql-bg-blue {
    background: #06c;
}
.ql-editor .ql-bg-purple {
    background: #93f;
}
.ql-editor .ql-color-white {
    color: #fff;
}
.ql-editor .ql-color-red {
    color: #e60000;
}
.ql-editor .ql-color-orange {
    color: #f90;
}
.ql-editor .ql-color-yellow {
    color: #ff0;
}
.ql-editor .ql-color-green {
    color: #008a00;
}
.ql-editor .ql-color-blue {
    color: #06c;
}
.ql-editor .ql-color-purple {
    color: #93f;
}
.ql-editor .ql-font-serif {
    font-family: Georgia, Times New Roman, serif;
}
.ql-editor .ql-font-monospace {
    font-family: Monaco, Courier New, monospace;
}
.ql-editor .ql-size-small {
    font-size: 0.75rem;
}
.ql-editor .ql-size-large {
    font-size: 1.5rem;
}
.ql-editor .ql-size-huge {
    font-size: 2.5rem;
}
.ql-editor .ql-direction-rtl {
    direction: rtl;
    text-align: inherit;
}
.ql-editor .ql-align-center {
    text-align: center;
}
.ql-editor .ql-align-justify {
    text-align: justify;
}
.ql-editor .ql-align-right {
    text-align: right;
}
.ql-editor.ql-blank::before {
    color: rgba(0, 0, 0, 0.6);
    content: attr(data-placeholder);
    font-style: italic;
    inset-inline-start: 15px;
    pointer-events: none;
    position: absolute;
    inset-inline-end: 15px;
}
.ql-snow.ql-toolbar:after,
.ql-snow .ql-toolbar:after {
    clear: both;
    content: '';
    display: table;
}
.ql-snow.ql-toolbar button,
.ql-snow .ql-toolbar button {
    background: none;
    border: none;
    cursor: pointer;
    display: inline-block;
    float: left;
    height: 24px;
    padding-block: 3px;
    padding-inline: 5px;
    width: 28px;
}
.ql-snow.ql-toolbar button svg,
.ql-snow .ql-toolbar button svg {
    float: left;
    height: 100%;
}
.ql-snow.ql-toolbar button:active:hover,
.ql-snow .ql-toolbar button:active:hover {
    outline: none;
}
.ql-snow.ql-toolbar input.ql-image[type='file'],
.ql-snow .ql-toolbar input.ql-image[type='file'] {
    display: none;
}
.ql-snow.ql-toolbar button:hover,
.ql-snow .ql-toolbar button:hover,
.ql-snow.ql-toolbar button:focus,
.ql-snow .ql-toolbar button:focus,
.ql-snow.ql-toolbar button.ql-active,
.ql-snow .ql-toolbar button.ql-active,
.ql-snow.ql-toolbar .ql-picker-label:hover,
.ql-snow .ql-toolbar .ql-picker-label:hover,
.ql-snow.ql-toolbar .ql-picker-label.ql-active,
.ql-snow .ql-toolbar .ql-picker-label.ql-active,
.ql-snow.ql-toolbar .ql-picker-item:hover,
.ql-snow .ql-toolbar .ql-picker-item:hover,
.ql-snow.ql-toolbar .ql-picker-item.ql-selected,
.ql-snow .ql-toolbar .ql-picker-item.ql-selected {
    color: #06c;
}
.ql-snow.ql-toolbar button:hover .ql-fill,
.ql-snow .ql-toolbar button:hover .ql-fill,
.ql-snow.ql-toolbar button:focus .ql-fill,
.ql-snow .ql-toolbar button:focus .ql-fill,
.ql-snow.ql-toolbar button.ql-active .ql-fill,
.ql-snow .ql-toolbar button.ql-active .ql-fill,
.ql-snow.ql-toolbar .ql-picker-label:hover .ql-fill,
.ql-snow .ql-toolbar .ql-picker-label:hover .ql-fill,
.ql-snow.ql-toolbar .ql-picker-label.ql-active .ql-fill,
.ql-snow .ql-toolbar .ql-picker-label.ql-active .ql-fill,
.ql-snow.ql-toolbar .ql-picker-item:hover .ql-fill,
.ql-snow .ql-toolbar .ql-picker-item:hover .ql-fill,
.ql-snow.ql-toolbar .ql-picker-item.ql-selected .ql-fill,
.ql-snow .ql-toolbar .ql-picker-item.ql-selected .ql-fill,
.ql-snow.ql-toolbar button:hover .ql-stroke.ql-fill,
.ql-snow .ql-toolbar button:hover .ql-stroke.ql-fill,
.ql-snow.ql-toolbar button:focus .ql-stroke.ql-fill,
.ql-snow .ql-toolbar button:focus .ql-stroke.ql-fill,
.ql-snow.ql-toolbar button.ql-active .ql-stroke.ql-fill,
.ql-snow .ql-toolbar button.ql-active .ql-stroke.ql-fill,
.ql-snow.ql-toolbar .ql-picker-label:hover .ql-stroke.ql-fill,
.ql-snow .ql-toolbar .ql-picker-label:hover .ql-stroke.ql-fill,
.ql-snow.ql-toolbar .ql-picker-label.ql-active .ql-stroke.ql-fill,
.ql-snow .ql-toolbar .ql-picker-label.ql-active .ql-stroke.ql-fill,
.ql-snow.ql-toolbar .ql-picker-item:hover .ql-stroke.ql-fill,
.ql-snow .ql-toolbar .ql-picker-item:hover .ql-stroke.ql-fill,
.ql-snow.ql-toolbar .ql-picker-item.ql-selected .ql-stroke.ql-fill,
.ql-snow .ql-toolbar .ql-picker-item.ql-selected .ql-stroke.ql-fill {
    fill: #06c;
}
.ql-snow.ql-toolbar button:hover .ql-stroke,
.ql-snow .ql-toolbar button:hover .ql-stroke,
.ql-snow.ql-toolbar button:focus .ql-stroke,
.ql-snow .ql-toolbar button:focus .ql-stroke,
.ql-snow.ql-toolbar button.ql-active .ql-stroke,
.ql-snow .ql-toolbar button.ql-active .ql-stroke,
.ql-snow.ql-toolbar .ql-picker-label:hover .ql-stroke,
.ql-snow .ql-toolbar .ql-picker-label:hover .ql-stroke,
.ql-snow.ql-toolbar .ql-picker-label.ql-active .ql-stroke,
.ql-snow .ql-toolbar .ql-picker-label.ql-active .ql-stroke,
.ql-snow.ql-toolbar .ql-picker-item:hover .ql-stroke,
.ql-snow .ql-toolbar .ql-picker-item:hover .ql-stroke,
.ql-snow.ql-toolbar .ql-picker-item.ql-selected .ql-stroke,
.ql-snow .ql-toolbar .ql-picker-item.ql-selected .ql-stroke,
.ql-snow.ql-toolbar button:hover .ql-stroke-miter,
.ql-snow .ql-toolbar button:hover .ql-stroke-miter,
.ql-snow.ql-toolbar button:focus .ql-stroke-miter,
.ql-snow .ql-toolbar button:focus .ql-stroke-miter,
.ql-snow.ql-toolbar button.ql-active .ql-stroke-miter,
.ql-snow.ql-toolbar button.ql-active .ql-stroke-miter,
.ql-snow.ql-toolbar .ql-picker-label:hover .ql-stroke-miter,
.ql-snow .ql-toolbar .ql-picker-label:hover .ql-stroke-miter,
.ql-snow.ql-toolbar .ql-picker-label.ql-active .ql-stroke-miter,
.ql-snow .ql-toolbar .ql-picker-label.ql-active .ql-stroke-miter,
.ql-snow.ql-toolbar .ql-picker-item:hover .ql-stroke-miter,
.ql-snow .ql-toolbar .ql-picker-item:hover .ql-stroke-miter,
.ql-snow.ql-toolbar .ql-picker-item.ql-selected .ql-stroke-miter,
.ql-snow .ql-toolbar .ql-picker-item.ql-selected .ql-stroke-miter {
    stroke: #06c;
}
@media (pointer: coarse) {
    .ql-snow.ql-toolbar button:hover:not(.ql-active),
    .ql-snow .ql-toolbar button:hover:not(.ql-active) {
        color: #444;
    }
    .ql-snow.ql-toolbar button:hover:not(.ql-active) .ql-fill,
    .ql-snow .ql-toolbar button:hover:not(.ql-active) .ql-fill,
    .ql-snow.ql-toolbar button:hover:not(.ql-active) .ql-stroke.ql-fill,
    .ql-snow .ql-toolbar button:hover:not(.ql-active) .ql-stroke.ql-fill {
        fill: #444;
    }
    .ql-snow.ql-toolbar button:hover:not(.ql-active) .ql-stroke,
    .ql-snow .ql-toolbar button:hover:not(.ql-active) .ql-stroke,
    .ql-snow.ql-toolbar button:hover:not(.ql-active) .ql-stroke-miter,
    .ql-snow .ql-toolbar button:hover:not(.ql-active) .ql-stroke-miter {
        stroke: #444;
    }
}
.ql-snow {
    box-sizing: border-box;
}
.ql-snow * {
    box-sizing: border-box;
}
.ql-snow .ql-hidden {
    display: none;
}
.ql-snow .ql-out-bottom,
.ql-snow .ql-out-top {
    visibility: hidden;
}
.ql-snow .ql-tooltip {
    position: absolute;
    transform: translateY(10px);
}
.ql-snow .ql-tooltip a {
    cursor: pointer;
    text-decoration: none;
}
.ql-snow .ql-tooltip.ql-flip {
    transform: translateY(-10px);
}
.ql-snow .ql-formats {
    display: inline-block;
    vertical-align: middle;
}
.ql-snow .ql-formats:after {
    clear: both;
    content: '';
    display: table;
}
.ql-snow .ql-stroke {
    fill: none;
    stroke: #444;
    stroke-linecap: round;
    stroke-linejoin: round;
    stroke-width: 2;
}
.ql-snow .ql-stroke-miter {
    fill: none;
    stroke: #444;
    stroke-miterlimit: 10;
    stroke-width: 2;
}
.ql-snow .ql-fill,
.ql-snow .ql-stroke.ql-fill {
    fill: #444;
}
.ql-snow .ql-empty {
    fill: none;
}
.ql-snow .ql-even {
    fill-rule: evenodd;
}
.ql-snow .ql-thin,
.ql-snow .ql-stroke.ql-thin {
    stroke-width: 1;
}
.ql-snow .ql-transparent {
    opacity: 0.4;
}
.ql-snow .ql-direction svg:last-child {
    display: none;
}
.ql-snow .ql-direction.ql-active svg:last-child {
    display: inline;
}
.ql-snow .ql-direction.ql-active svg:first-child {
    display: none;
}
.ql-snow .ql-editor h1 {
    font-size: 2rem;
}
.ql-snow .ql-editor h2 {
    font-size: 1.5rem;
}
.ql-snow .ql-editor h3 {
    font-size: 1.17rem;
}
.ql-snow .ql-editor h4 {
    font-size: 1rem;
}
.ql-snow .ql-editor h5 {
    font-size: 0.83rem;
}
.ql-snow .ql-editor h6 {
    font-size: 0.67rem;
}
.ql-snow .ql-editor a {
    text-decoration: underline;
}
.ql-snow .ql-editor blockquote {
    border-inline-start: 4px solid #ccc;
    margin-block-end: 5px;
    margin-block-start: 5px;
    padding-inline-start: 16px;
}
.ql-snow .ql-editor code,
.ql-snow .ql-editor pre {
    background: #f0f0f0;
    border-radius: 3px;
}
.ql-snow .ql-editor pre {
    white-space: pre-wrap;
    margin-block-end: 5px;
    margin-block-start: 5px;
    padding: 5px 10px;
}
.ql-snow .ql-editor code {
    font-size: 85%;
    padding: 2px 4px;
}
.ql-snow .ql-editor pre.ql-syntax {
    background: #23241f;
    color: #f8f8f2;
    overflow: visible;
}
.ql-snow .ql-editor img {
    max-width: 100%;
}
.ql-snow .ql-picker {
    color: #444;
    display: inline-block;
    float: left;
    inset-inline-start: 0;
    font-size: 14px;
    font-weight: 500;
    height: 24px;
    position: relative;
    vertical-align: middle;
}
.ql-snow .ql-picker-label {
    cursor: pointer;
    display: inline-block;
    height: 100%;
    padding-inline-start: 8px;
    padding-inline-end: 2px;
    position: relative;
    width: 100%;
}
.ql-snow .ql-picker-label::before {
    display: inline-block;
    line-height: 22px;
}
.ql-snow .ql-picker-options {
    background: #fff;
    display: none;
    min-width: 100%;
    padding: 4px 8px;
    position: absolute;
    white-space: nowrap;
}
.ql-snow .ql-picker-options .ql-picker-item {
    cursor: pointer;
    display: block;
    padding-block-end: 5px;
    padding-block-start: 5px;
}
.ql-snow .ql-picker.ql-expanded .ql-picker-label {
    color: #ccc;
    z-index: 2;
}
.ql-snow .ql-picker.ql-expanded .ql-picker-label .ql-fill {
    fill: #ccc;
}
.ql-snow .ql-picker.ql-expanded .ql-picker-label .ql-stroke {
    stroke: #ccc;
}
.ql-snow .ql-picker.ql-expanded .ql-picker-options {
    display: block;
    margin-block-start: -1px;
    top: 100%;
    z-index: 1;
}
.ql-snow .ql-color-picker,
.ql-snow .ql-icon-picker {
    width: 28px;
}
.ql-snow .ql-color-picker .ql-picker-label,
.ql-snow .ql-icon-picker .ql-picker-label {
    padding: 2px 4px;
}
.ql-snow .ql-color-picker .ql-picker-label svg,
.ql-snow .ql-icon-picker .ql-picker-label svg {
    inset-inline-end: 4px;
}
.ql-snow .ql-icon-picker .ql-picker-options {
    padding: 4px 0;
}
.ql-snow .ql-icon-picker .ql-picker-item {
    height: 24px;
    width: 24px;
    padding: 2px 4px;
}
.ql-snow .ql-color-picker .ql-picker-options {
    padding: 3px 5px;
    width: 152px;
}
.ql-snow .ql-color-picker .ql-picker-item {
    border: 1px solid transparent;
    float: left;
    height: 16px;
    margin: 2px;
    padding: 0;
    width: 16px;
}
.ql-snow .ql-picker:not(.ql-color-picker):not(.ql-icon-picker) svg {
    position: absolute;
    margin-block-start: -9px;
    inset-inline-end: 0;
    top: 50%;
    width: 18px;
}
.ql-snow .ql-picker.ql-header .ql-picker-label[data-label]:not([data-label=''])::before,
.ql-snow .ql-picker.ql-font .ql-picker-label[data-label]:not([data-label=''])::before,
.ql-snow .ql-picker.ql-size .ql-picker-label[data-label]:not([data-label=''])::before,
.ql-snow .ql-picker.ql-header .ql-picker-item[data-label]:not([data-label=''])::before,
.ql-snow .ql-picker.ql-font .ql-picker-item[data-label]:not([data-label=''])::before,
.ql-snow .ql-picker.ql-size .ql-picker-item[data-label]:not([data-label=''])::before {
    content: attr(data-label);
}
.ql-snow .ql-picker.ql-header {
    width: 98px;
}
.ql-snow .ql-picker.ql-header .ql-picker-label::before,
.ql-snow .ql-picker.ql-header .ql-picker-item::before {
    content: 'Normal';
}
.ql-snow .ql-picker.ql-header .ql-picker-label[data-value='1']::before,
.ql-snow .ql-picker.ql-header .ql-picker-item[data-value='1']::before {
    content: 'Heading 1';
}
.ql-snow .ql-picker.ql-header .ql-picker-label[data-value='2']::before,
.ql-snow .ql-picker.ql-header .ql-picker-item[data-value='2']::before {
    content: 'Heading 2';
}
.ql-snow .ql-picker.ql-header .ql-picker-label[data-value='3']::before,
.ql-snow .ql-picker.ql-header .ql-picker-item[data-value='3']::before {
    content: 'Heading 3';
}
.ql-snow .ql-picker.ql-header .ql-picker-label[data-value='4']::before,
.ql-snow .ql-picker.ql-header .ql-picker-item[data-value='4']::before {
    content: 'Heading 4';
}
.ql-snow .ql-picker.ql-header .ql-picker-label[data-value='5']::before,
.ql-snow .ql-picker.ql-header .ql-picker-item[data-value='5']::before {
    content: 'Heading 5';
}
.ql-snow .ql-picker.ql-header .ql-picker-label[data-value='6']::before,
.ql-snow .ql-picker.ql-header .ql-picker-item[data-value='6']::before {
    content: 'Heading 6';
}
.ql-snow .ql-picker.ql-header .ql-picker-item[data-value='1']::before {
    font-size: 2rem;
}
.ql-snow .ql-picker.ql-header .ql-picker-item[data-value='2']::before {
    font-size: 1.5rem;
}
.ql-snow .ql-picker.ql-header .ql-picker-item[data-value='3']::before {
    font-size: 1.17rem;
}
.ql-snow .ql-picker.ql-header .ql-picker-item[data-value='4']::before {
    font-size: 1rem;
}
.ql-snow .ql-picker.ql-header .ql-picker-item[data-value='5']::before {
    font-size: 0.83rem;
}
.ql-snow .ql-picker.ql-header .ql-picker-item[data-value='6']::before {
    font-size: 0.67rem;
}
.ql-snow .ql-picker.ql-font {
    width: 108px;
}
.ql-snow .ql-picker.ql-font .ql-picker-label::before,
.ql-snow .ql-picker.ql-font .ql-picker-item::before {
    content: 'Sans Serif';
}
.ql-snow .ql-picker.ql-font .ql-picker-label[data-value='serif']::before,
.ql-snow .ql-picker.ql-font .ql-picker-item[data-value='serif']::before {
    content: 'Serif';
}
.ql-snow .ql-picker.ql-font .ql-picker-label[data-value='monospace']::before,
.ql-snow .ql-picker.ql-font .ql-picker-item[data-value='monospace']::before {
    content: 'Monospace';
}
.ql-snow .ql-picker.ql-font .ql-picker-item[data-value='serif']::before {
    font-family: Georgia, Times New Roman, serif;
}
.ql-snow .ql-picker.ql-font .ql-picker-item[data-value='monospace']::before {
    font-family: Monaco, Courier New, monospace;
}
.ql-snow .ql-picker.ql-size {
    width: 98px;
}
.ql-snow .ql-picker.ql-size .ql-picker-label::before,
.ql-snow .ql-picker.ql-size .ql-picker-item::before {
    content: 'Normal';
}
.ql-snow .ql-picker.ql-size .ql-picker-label[data-value='small']::before,
.ql-snow .ql-picker.ql-size .ql-picker-item[data-value='small']::before {
    content: 'Small';
}
.ql-snow .ql-picker.ql-size .ql-picker-label[data-value='large']::before,
.ql-snow .ql-picker.ql-size .ql-picker-item[data-value='large']::before {
    content: 'Large';
}
.ql-snow .ql-picker.ql-size .ql-picker-label[data-value='huge']::before,
.ql-snow .ql-picker.ql-size .ql-picker-item[data-value='huge']::before {
    content: 'Huge';
}
.ql-snow .ql-picker.ql-size .ql-picker-item[data-value='small']::before {
    font-size: 10px;
}
.ql-snow .ql-picker.ql-size .ql-picker-item[data-value='large']::before {
    font-size: 18px;
}
.ql-snow .ql-picker.ql-size .ql-picker-item[data-value='huge']::before {
    font-size: 32px;
}
.ql-snow .ql-color-picker.ql-background .ql-picker-item {
    background: #fff;
}
.ql-snow .ql-color-picker.ql-color .ql-picker-item {
    background: #000;
}
.ql-toolbar.ql-snow {
    border: 1px solid #ccc;
    box-sizing: border-box;
    font-family: 'Helvetica Neue', 'Helvetica', 'Arial', sans-serif;
    padding: 8px;
}
.ql-toolbar.ql-snow .ql-formats {
    margin-inline-end: 15px;
}
.ql-toolbar.ql-snow .ql-picker-label {
    border: 1px solid transparent;
}
.ql-toolbar.ql-snow .ql-picker-options {
    border: 1px solid transparent;
    box-shadow: rgba(0, 0, 0, 0.2) 0 2px 8px;
}
.ql-toolbar.ql-snow .ql-picker.ql-expanded .ql-picker-label {
    border-color: #ccc;
}
.ql-toolbar.ql-snow .ql-picker.ql-expanded .ql-picker-options {
    border-color: #ccc;
}
.ql-toolbar.ql-snow .ql-color-picker .ql-picker-item.ql-selected,
.ql-toolbar.ql-snow .ql-color-picker .ql-picker-item:hover {
    border-color: #000;
}
.ql-toolbar.ql-snow + .ql-container.ql-snow {
    border-block-start: 0;
}
.ql-snow .ql-tooltip {
    background: #fff;
    border: 1px solid #ccc;
    box-shadow: 0 0 5px #ddd;
    color: #444;
    padding: 5px 12px;
    white-space: nowrap;
}
.ql-snow .ql-tooltip::before {
    content: 'Visit URL:';
    line-height: 26px;
    margin-inline-end: 8px;
}
.ql-snow .ql-tooltip input[type='text'] {
    display: none;
    border: 1px solid #ccc;
    font-size: 13px;
    height: 26px;
    margin: 0;
    padding: 3px 5px;
    width: 170px;
}
.ql-snow .ql-tooltip a.ql-preview {
    display: inline-block;
    max-width: 200px;
    overflow-x: hidden;
    text-overflow: ellipsis;
    vertical-align: top;
}
.ql-snow .ql-tooltip a.ql-action::after {
    border-inline-end: 1px solid #ccc;
    content: 'Edit';
    margin-inline-start: 16px;
    padding-inline-end: 8px;
}
.ql-snow .ql-tooltip a.ql-remove::before {
    content: 'Remove';
    margin-inline-start: 8px;
}
.ql-snow .ql-tooltip a {
    line-height: 26px;
}
.ql-snow .ql-tooltip.ql-editing a.ql-preview,
.ql-snow .ql-tooltip.ql-editing a.ql-remove {
    display: none;
}
.ql-snow .ql-tooltip.ql-editing input[type='text'] {
    display: inline-block;
}
.ql-snow .ql-tooltip.ql-editing a.ql-action::after {
    border-inline-end: 0;
    content: 'Save';
    padding-inline-end: 0;
}
.ql-snow .ql-tooltip[data-mode='link']::before {
    content: 'Enter link:';
}
.ql-snow .ql-tooltip[data-mode='formula']::before {
    content: 'Enter formula:';
}
.ql-snow .ql-tooltip[data-mode='video']::before {
    content: 'Enter video:';
}
.ql-snow a {
    color: #06c;
}
.ql-container.ql-snow {
    border: 1px solid #ccc;
}

.p-editor .p-editor-toolbar {
    background: ${t("editor.toolbar.background")};
    border-start-end-radius: ${t("editor.toolbar.border.radius")};
    border-start-start-radius: ${t("editor.toolbar.border.radius")};
}

.p-editor .p-editor-toolbar.ql-snow {
    border: 1px solid ${t("editor.toolbar.border.color")};
}

.p-editor .p-editor-toolbar.ql-snow .ql-stroke {
    stroke: ${t("editor.toolbar.item.color")};
}

.p-editor .p-editor-toolbar.ql-snow .ql-fill {
    fill: ${t("editor.toolbar.item.color")};
}

.p-editor .p-editor-toolbar.ql-snow .ql-picker .ql-picker-label {
    border: 0 none;
    color: ${t("editor.toolbar.item.color")};
}

.p-editor .p-editor-toolbar.ql-snow .ql-picker .ql-picker-label:hover {
    color: ${t("editor.toolbar.item.hover.color")};
}

.p-editor .p-editor-toolbar.ql-snow .ql-picker .ql-picker-label:hover .ql-stroke {
    stroke: ${t("editor.toolbar.item.hover.color")};
}

.p-editor .p-editor-toolbar.ql-snow .ql-picker .ql-picker-label:hover .ql-fill {
    fill: ${t("editor.toolbar.item.hover.color")};
}

.p-editor .p-editor-toolbar.ql-snow .ql-picker.ql-expanded .ql-picker-label {
    color: ${t("editor.toolbar.item.active.color")};
}

.p-editor .p-editor-toolbar.ql-snow .ql-picker.ql-expanded .ql-picker-label .ql-stroke {
    stroke: ${t("editor.toolbar.item.active.color")};
}

.p-editor .p-editor-toolbar.ql-snow .ql-picker.ql-expanded .ql-picker-label .ql-fill {
    fill: ${t("editor.toolbar.item.active.color")};
}

.p-editor .p-editor-toolbar.ql-snow .ql-picker.ql-expanded .ql-picker-options {
    background: ${t("editor.overlay.background")};
    border: 1px solid ${t("editor.overlay.border.color")};
    box-shadow: ${t("editor.overlay.shadow")};
    border-radius: ${t("editor.overlay.border.radius")};
    padding: ${t("editor.overlay.padding")};
}

.p-editor .p-editor-toolbar.ql-snow .ql-picker.ql-expanded .ql-picker-options .ql-picker-item {
    color: ${t("editor.overlay.option.color")};
    border-radius: ${t("editor.overlay.option.border.radius")};
}

.p-editor .p-editor-toolbar.ql-snow .ql-picker.ql-expanded .ql-picker-options .ql-picker-item:hover {
    background: ${t("editor.overlay.option.focus.background")};
    color: ${t("editor.overlay.option.focus.color")};
}

.p-editor .p-editor-toolbar.ql-snow .ql-picker.ql-expanded:not(.ql-icon-picker) .ql-picker-item {
    padding: ${t("editor.overlay.option.padding")};
}

.p-editor .p-editor-content {
    border-end-end-radius: ${t("editor.content.border.radius")};
    border-end-start-radius: ${t("editor.content.border.radius")};
}

.p-editor .p-editor-content.ql-snow {
    border: 1px solid ${t("editor.content.border.color")};
}

.p-editor .p-editor-content .ql-editor {
    background: ${t("editor.content.background")};
    color: ${t("editor.content.color")};
    border-end-end-radius: ${t("editor.content.border.radius")};
    border-end-start-radius: ${t("editor.content.border.radius")};
}

.p-editor .ql-snow.ql-toolbar button:hover,
.p-editor .ql-snow.ql-toolbar button:focus {
    color: ${t("editor.toolbar.item.hover.color")};
}

.p-editor .ql-snow.ql-toolbar button:hover .ql-stroke,
.p-editor .ql-snow.ql-toolbar button:focus .ql-stroke {
    stroke: ${t("editor.toolbar.item.hover.color")};
}

.p-editor .ql-snow.ql-toolbar button:hover .ql-fill,
.p-editor .ql-snow.ql-toolbar button:focus .ql-fill {
    fill: ${t("editor.toolbar.item.hover.color")};
}

.p-editor .ql-snow.ql-toolbar button.ql-active,
.p-editor .ql-snow.ql-toolbar .ql-picker-label.ql-active,
.p-editor .ql-snow.ql-toolbar .ql-picker-item.ql-selected {
    color: ${t("editor.toolbar.item.active.color")};
}

.p-editor .ql-snow.ql-toolbar button.ql-active .ql-stroke,
.p-editor .ql-snow.ql-toolbar .ql-picker-label.ql-active .ql-stroke,
.p-editor .ql-snow.ql-toolbar .ql-picker-item.ql-selected .ql-stroke {
    stroke: ${t("editor.toolbar.item.active.color")};
}

.p-editor .ql-snow.ql-toolbar button.ql-active .ql-fill,
.p-editor .ql-snow.ql-toolbar .ql-picker-label.ql-active .ql-fill,
.p-editor .ql-snow.ql-toolbar .ql-picker-item.ql-selected .ql-fill {
    fill: ${t("editor.toolbar.item.active.color")};
}

.p-editor .ql-snow.ql-toolbar button.ql-active .ql-picker-label,
.p-editor .ql-snow.ql-toolbar .ql-picker-label.ql-active .ql-picker-label,
.p-editor .ql-snow.ql-toolbar .ql-picker-item.ql-selected .ql-picker-label {
    color: ${t("editor.toolbar.item.active.color")};
}
`,Re={root:function(e){var o=e.instance;return["p-editor",{"p-invalid":o.$invalid}]},toolbar:"p-editor-toolbar",content:"p-editor-content"},je=ee.extend({name:"editor",style:Ue,classes:Re}),Ne={name:"BaseEditor",extends:Se,props:{placeholder:String,readonly:Boolean,formats:Array,editorStyle:null,modules:null},style:je,provide:function(){return{$pcEditor:this,$parentInstance:this}}};function _(t){"@babel/helpers - typeof";return _=typeof Symbol=="function"&&typeof Symbol.iterator=="symbol"?function(e){return typeof e}:function(e){return e&&typeof Symbol=="function"&&e.constructor===Symbol&&e!==Symbol.prototype?"symbol":typeof e},_(t)}function Y(t,e){var o=Object.keys(t);if(Object.getOwnPropertySymbols){var l=Object.getOwnPropertySymbols(t);e&&(l=l.filter(function(n){return Object.getOwnPropertyDescriptor(t,n).enumerable})),o.push.apply(o,l)}return o}function We(t){for(var e=1;e<arguments.length;e++){var o=arguments[e]!=null?arguments[e]:{};e%2?Y(Object(o),!0).forEach(function(l){Qe(t,l,o[l])}):Object.getOwnPropertyDescriptors?Object.defineProperties(t,Object.getOwnPropertyDescriptors(o)):Y(Object(o)).forEach(function(l){Object.defineProperty(t,l,Object.getOwnPropertyDescriptor(o,l))})}return t}function Qe(t,e,o){return(e=Ge(e))in t?Object.defineProperty(t,e,{value:o,enumerable:!0,configurable:!0,writable:!0}):t[e]=o,t}function Ge(t){var e=Ke(t,"string");return _(e)=="symbol"?e:e+""}function Ke(t,e){if(_(t)!="object"||!t)return t;var o=t[Symbol.toPrimitive];if(o!==void 0){var l=o.call(t,e);if(_(l)!="object")return l;throw new TypeError("@@toPrimitive must return a primitive value.")}return(e==="string"?String:Number)(t)}var J=function(){try{return window.Quill}catch{return null}}(),ne={name:"Editor",extends:Ne,inheritAttrs:!1,emits:["text-change","selection-change","load"],quill:null,watch:{modelValue:function(e,o){e!==o&&this.quill&&!this.quill.hasFocus()&&this.renderValue(e)},readonly:function(){this.handleReadOnlyChange()}},mounted:function(){var e=this,o={modules:We({toolbar:this.$refs.toolbarElement},this.modules),readOnly:this.readonly,theme:"snow",formats:this.formats,placeholder:this.placeholder};J?(this.quill=new J(this.$refs.editorElement,o),this.initQuill(),this.handleLoad()):ue(()=>import("./BoNxg3xf.js"),__vite__mapDeps([0,1,2,3,4,5,6,7,8,9,10,11]),import.meta.url).then(function(l){l&&te(e.$refs.editorElement)&&(l.default?e.quill=new l.default(e.$refs.editorElement,o):e.quill=new l(e.$refs.editorElement,o),e.initQuill())}).then(function(){e.handleLoad()})},beforeUnmount:function(){this.quill=null},methods:{renderValue:function(e){if(this.quill)if(e){var o=this.quill.clipboard.convert({html:e});this.quill.setContents(o)}else this.quill.setText("")},initQuill:function(){var e=this;this.renderValue(this.d_value),this.quill.on("text-change",function(o,l,n){if(n==="user"){var i=e.quill.getSemanticHTML(),d=e.quill.getText().trim();i==="<p><br></p>"&&(i=""),e.writeValue(i),e.$emit("text-change",{htmlValue:i,textValue:d,delta:o,source:n,instance:e.quill})}}),this.quill.on("selection-change",function(o,l,n){var i=e.quill.getSemanticHTML(),d=e.quill.getText().trim();e.$emit("selection-change",{htmlValue:i,textValue:d,range:o,oldRange:l,source:n,instance:e.quill})})},handleLoad:function(){this.quill&&this.quill.getModule("toolbar")&&this.$emit("load",{instance:this.quill})},handleReadOnlyChange:function(){this.quill&&this.quill.enable(!this.readonly)}}};function Ze(t,e,o,l,n,i){return L(),I("div",c({class:t.cx("root")},t.ptmi("root")),[r("div",c({ref:"toolbarElement",class:t.cx("toolbar")},t.ptm("toolbar")),[qe(t.$slots,"toolbar",{},function(){return[r("span",c({class:"ql-formats"},t.ptm("formats")),[r("select",c({class:"ql-header",defaultValue:"0"},t.ptm("header")),[r("option",c({value:"1"},t.ptm("option")),"Heading",16),r("option",c({value:"2"},t.ptm("option")),"Subheading",16),r("option",c({value:"0"},t.ptm("option")),"Normal",16)],16),r("select",c({class:"ql-font"},t.ptm("font")),[r("option",me(be(t.ptm("option"))),null,16),r("option",c({value:"serif"},t.ptm("option")),null,16),r("option",c({value:"monospace"},t.ptm("option")),null,16)],16)],16),r("span",c({class:"ql-formats"},t.ptm("formats")),[r("button",c({class:"ql-bold",type:"button"},t.ptm("bold")),null,16),r("button",c({class:"ql-italic",type:"button"},t.ptm("italic")),null,16),r("button",c({class:"ql-underline",type:"button"},t.ptm("underline")),null,16)],16),r("span",c({class:"ql-formats"},t.ptm("formats")),[r("select",c({class:"ql-color"},t.ptm("color")),null,16),r("select",c({class:"ql-background"},t.ptm("background")),null,16)],16),r("span",c({class:"ql-formats"},t.ptm("formats")),[r("button",c({class:"ql-list",value:"ordered",type:"button"},t.ptm("list")),null,16),r("button",c({class:"ql-list",value:"bullet",type:"button"},t.ptm("list")),null,16),r("select",c({class:"ql-align"},t.ptm("select")),[r("option",c({defaultValue:""},t.ptm("option")),null,16),r("option",c({value:"center"},t.ptm("option")),null,16),r("option",c({value:"right"},t.ptm("option")),null,16),r("option",c({value:"justify"},t.ptm("option")),null,16)],16)],16),r("span",c({class:"ql-formats"},t.ptm("formats")),[r("button",c({class:"ql-link",type:"button"},t.ptm("link")),null,16),r("button",c({class:"ql-image",type:"button"},t.ptm("image")),null,16),r("button",c({class:"ql-code-block",type:"button"},t.ptm("codeBlock")),null,16)],16),r("span",c({class:"ql-formats"},t.ptm("formats")),[r("button",c({class:"ql-clean",type:"button"},t.ptm("clean")),null,16)],16)]})],16),r("div",c({ref:"editorElement",class:t.cx("content"),style:t.editorStyle},t.ptm("content")),null,16)],16)}ne.render=Ze;const Ye={class:"mb-3"},Je={class:"ql-formats"},Xe={class:"ql-bold"},et={class:"ql-italic"},tt={class:"ql-underline"},ot={class:"ql-header"},X=oe({__name:"BaseControlEditorInput",props:{modelValue:{type:String,default:""},xmlColumn:{type:Object,required:!0}},emits:["update:modelValue"],setup(t,{emit:e}){const o=t,l=e,n=le(o.modelValue),i={modules:{toolbar:[["bold","italic","underline"],[{header:[1,2,3,!1]}],[{font:[]}]]}};return F(()=>o.modelValue,d=>{n.value=d}),F(n,d=>{l("update:modelValue",d)}),(d,s)=>{const a=Fe;return L(),I("div",Ye,[u(Ce,{"xml-column":t.xmlColumn},null,8,["xml-column"]),u(p(ne),{modelValue:n.value,"onUpdate:modelValue":s[0]||(s[0]=q=>n.value=q),editorConfig:i,editorStyle:"height: 320px"},{toolbar:T(()=>[r("span",Je,[S(r("button",Xe,null,512),[[a,"Bold",void 0,{bottom:!0}]]),S(r("button",et,null,512),[[a,"Italic",void 0,{bottom:!0}]]),S(r("button",tt,null,512),[[a,"Underline",void 0,{bottom:!0}]]),S((L(),I("select",ot,s[1]||(s[1]=[r("option",{selected:""},null,-1),r("option",{value:"1"},"Heading 1",-1),r("option",{value:"2"},"Heading 2",-1),r("option",{value:"3"},"Heading 3",-1)]))),[[a,"Heading",void 0,{bottom:!0}]]),s[2]||(s[2]=r("select",{class:"ql-font"},[r("option",{selected:""}),r("option",{value:"monospace"},"Monospace")],-1))])]),_:1},8,["modelValue"])])}}}),A={quantity:"1",description:"",name:"",price:"",discount:"",shortDescription:"",parentCategory:"",childCategory:"",quantityUpdate:"1",descriptionUpdate:"",nameUpdate:"",priceUpdate:"",discountUpdate:"",shortDescriptionUpdate:"",parentCategoryUpdate:"",childCategoryUpdate:""},lt={...A},nt={values:A,errors:lt,...he},it=fe("MPS",{state:()=>({fields:nt,accessoriesList:[]}),getters:{fieldValues:t=>t.fields.values,fieldErrors:t=>t.fields.errors},actions:{SetFields(t){this.fields=t},ResetStore(){this.fields.resetForm()},async GetAllProduct(){const t=C(),e=E();e.LoadingChange(!0);const o=await t.api.v1.MPSSelectAccessories.$get({});return o.success?(e.LoadingChange(!1),this.accessoriesList=o.response??[],!0):!1},async DeleteOneProduct(t){const e=C(),o=E();o.LoadingChange(!0);const l=P(),n=await e.api.v1.MPSDeleteAccessory.$patch({body:{codeAccessory:[t]}});return o.LoadingChange(!1),n.success?(l.SetFormMessage(n,!0),!0):(l.SetFormMessage(n,!0),!1)},async AddProduct(t){const e=G(),o=C(),l=E(),n=P(),i=V(),d=U(this.fields.values,A),s=i.uploadImage.map((q,b)=>R(q.imagePreview,`image_${b+1}.png`));console.log("Discount",d.discount),console.log("Name",d.name),l.LoadingChange(!0);const a=await o.api.v1.MPSInsertAccessory.$post({body:{Images:s},query:{CategoryId:await e.CheckCategory()?e.chooseCategoryId:"",Description:t,Discount:Number(d.discount),Name:d.name,Price:Number(d.price),Quantity:Number(d.quantity),ShortDescription:d.shortDescription}});return e.ResetCategory(),l.LoadingChange(!1),a.success?(i.ResetStore(),n.SetFormMessage(a,!0),!0):(n.SetFormMessage(a,!0),!1)},async UpdateProduct(t,e,o){const l=G(),n=C(),i=E();i.LoadingChange(!0);const d=P(),s=V(),a=U(this.fields.values,A),q=s.uploadImage.map((y,D)=>R(y.imagePreview,`image_${D+1}.png`)),b=await n.api.v1.MPSUpdateAccessory.$put({body:{Images:q},query:{CodeAccessory:e,CategoryId:await l.CheckCategory()?l.chooseCategoryId:"",Description:t,Discount:Number(a.discount),Name:a.name,Price:Number(a.price),Quantity:Number(a.quantity),ShortDescription:a.shortDescription,ImageId:o}});return i.LoadingChange(!1),b.success?(s.ResetStore(),d.SetFormMessage(b,!0),!0):(d.SetFormMessage(b,!0),!1)}}}),rt={class:"animate-fade-left animate-ease-out animate-delay-100 p-10 md:max-w-none md:p-12 lg:p-30 rounded-xl mb-16 max-w-screen-lg mx-auto h-auto overflow-hidden"},at={class:"grid grid-cols-2 gap-6 mb-8"},st={class:"grid grid-cols-2 gap-6 mb-8"},Wt=oe({__name:"index",setup(t){const e=it(),o=V(),l=le(""),{fieldValues:n,fieldErrors:i}=ge(e),d=ve({initialValues:n.value});e.SetFields(d);const s={quantity:w({id:"quantity",name:"Quantity",rules:"required",visible:!0,option:""}),name:w({id:"name",name:"Product Name",rules:"required",visible:!0,option:""}),price:w({id:"price",name:"Product Price",rules:"required|min_max:0,9999999999",visible:!0,option:""}),discount:w({id:"discount",name:"Discount",rules:"required|min_max:0,100",visible:!0,option:""}),description:w({id:"description",name:"Description",rules:"required",visible:!0,option:""}),shortDescription:w({id:"shortDescription",name:"Short Description",rules:"required",visible:!0,option:""}),parentCategory:w({id:"parentCategory",name:"Parent Category",rules:"",visible:!0,option:""}),childCategory:w({id:"childCategory",name:"Child Category",rules:"",visible:!0,option:""})},a=[{field:"accessoryId",header:"Accessory Id",isFilter:!0,isSort:!0,filterType:"text"},{field:"accessoryName",header:"Accessory Name",isFilter:!0,isSort:!0,filterType:"text"},{field:"description",header:"Description",isFilter:!0,isSort:!0,filterType:"text"},{field:"price",header:"Price",isFilter:!0,isSort:!0,filterType:"text"},{field:"discount",header:"Discount",isFilter:!0,isSort:!0,filterType:"text"},{field:"quantity",header:"Quantity",isFilter:!0,isSort:!0,filterType:"text"},{field:"totalReviews",header:"Total Reviews",isFilter:!0,isSort:!0,filterType:"text"},{field:"totalSold",header:"Total Sold",isFilter:!0,isSort:!0,filterType:"text"},{field:"imageAccessoriesList",header:"Images",isFilter:!1,isSort:!1,filterType:""}],q=async m=>{await ye(),e.fields.setFieldValue("quantity",m.quantity),e.fields.setFieldValue("name",m.accessoryName),e.fields.setFieldValue("price",m.price),e.fields.setFieldValue("discount",m.discount),e.fields.setFieldValue("shortDescription",m.shortDescription),e.fields.setFieldValue("parentCategory",m.parentCategoryName),e.fields.setFieldValue("childCategory",m.childCategoryName);const f=await Promise.all(m.imageAccessoriesList.map(async v=>await xe(v.imageUrl)));o.SetImage(f),l.value=m.description},b=async m=>{await e.DeleteOneProduct(m.accessoryId),await e.GetAllProduct()},y=async()=>{await e.AddProduct(l.value),await e.GetAllProduct()},D=async m=>{const f=m.imageAccessoriesList.map(v=>v.imageId);await e.UpdateProduct(l.value,m.accessoryId,f),await e.GetAllProduct()};return we(async()=>{await e.GetAllProduct()}),(m,f)=>(L(),ke(Oe,null,{body:T(()=>[f[3]||(f[3]=r("div",{class:"max-w-3xl text-start relative z-10 mt-0 pt-0 font-sans leading-relaxed text-gray-900 dark:text-white"},[r("h1",{class:"text-4xl font-medium tracking-wide text-transparent bg-clip-text bg-gradient-to-r from-blue-600 to-indigo-600 mb-4 animate-fade-up animate-duration-1000 animate-delay-300"}," Manage Product "),r("p",{class:"text-base sm:text-lg text-gray-700 dark:text-gray-300 mb-8 animate-fade-up animate-duration-1000 animate-delay-500"}," Welcome, Admin! ")],-1)),r("div",rt,[u(Ee,{items:p(e).accessoriesList,columns:a,"is-selected-columns":!0,onOnDelete:b,onOnInsert:y,onOnBindingInsertData:f[2]||(f[2]=v=>p(o).ResetStore()),onOnBindingUpdateData:q,onOnUpdate:D,"default-fields-select":["accessoryId","accessoryName","price","imageAccessoriesList"]},{bodyButtonAdd:T(()=>[r("div",at,[r("div",null,[u(g,{"xml-column":s.name,maxlength:50,disabled:!1,"err-msg":p(i).name},null,8,["xml-column","err-msg"]),u(g,{"xml-column":s.quantity,maxlength:50,disabled:!1,"err-msg":p(i).quantity,"is-numberic":!0,"is-not-phone-number":!0},null,8,["xml-column","err-msg"])]),r("div",null,[u(g,{"xml-column":s.price,maxlength:50,disabled:!1,"err-msg":p(i).price,"is-numberic":!0,"is-not-phone-number":!0},null,8,["xml-column","err-msg"]),u(g,{"xml-column":s.discount,maxlength:50,disabled:!1,"err-msg":p(i).discount,"is-numberic":!0,"is-not-phone-number":!0},null,8,["xml-column","err-msg"])])]),u(W,{"xml-column-parent-category":s.parentCategory,"maxlength-parent-category":50,"disabled-parent-category":!1,"err-msg-parent-category":p(i).parentCategory,"placeholder-parent-category":"Parent Category","xml-column-child-category":s.childCategory,"maxlength-child-category":50,"disabled-child-category":!1,"err-msg-child-category":p(i).childCategory,"placeholder-child-category":"Child Category"},null,8,["xml-column-parent-category","err-msg-parent-category","xml-column-child-category","err-msg-child-category"]),u(Q,{"max-number-image":5,"is-show-popover":!0,label:"Upload avatar image"}),u(X,{"xml-column":s.description,modelValue:p(l),"onUpdate:modelValue":f[0]||(f[0]=v=>j(l)?l.value=v:null)},null,8,["xml-column","modelValue"]),u(g,{"xml-column":s.shortDescription,maxlength:50,disabled:!1,"err-msg":p(i).shortDescription},null,8,["xml-column","err-msg"])]),bodyButtonUpdate:T(()=>[r("div",st,[r("div",null,[u(g,{"xml-column":s.name,maxlength:50,disabled:!1,"err-msg":p(i).name},null,8,["xml-column","err-msg"]),u(g,{"xml-column":s.quantity,maxlength:50,disabled:!1,"err-msg":p(i).quantity,"is-numberic":!0,"is-not-phone-number":!0},null,8,["xml-column","err-msg"])]),r("div",null,[u(g,{"xml-column":s.price,maxlength:50,disabled:!1,"err-msg":p(i).price,"is-numberic":!0,"is-not-phone-number":!0},null,8,["xml-column","err-msg"]),u(g,{"xml-column":s.discount,maxlength:50,disabled:!1,"err-msg":p(i).discount,"is-numberic":!0,"is-not-phone-number":!0},null,8,["xml-column","err-msg"])])]),u(W,{"xml-column-parent-category":s.parentCategory,"maxlength-parent-category":50,"disabled-parent-category":!1,"err-msg-parent-category":p(i).parentCategory,"placeholder-parent-category":"Parent Category","xml-column-child-category":s.childCategory,"maxlength-child-category":50,"disabled-child-category":!1,"err-msg-child-category":p(i).childCategory,"placeholder-child-category":"Child Category"},null,8,["xml-column-parent-category","err-msg-parent-category","xml-column-child-category","err-msg-child-category"]),u(Q,{"max-number-image":5,"is-show-popover":!0,label:"Upload avatar image"}),u(X,{"xml-column":s.description,modelValue:p(l),"onUpdate:modelValue":f[1]||(f[1]=v=>j(l)?l.value=v:null)},null,8,["xml-column","modelValue"]),u(g,{"xml-column":s.shortDescription,maxlength:50,disabled:!1,"err-msg":p(i).shortDescription},null,8,["xml-column","err-msg"])]),_:1},8,["items"])])]),_:1}))}});export{Wt as default};
