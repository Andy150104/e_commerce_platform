import{s as g}from"./DqbQgBaR.js";import{s as h}from"./BuMp5kCj.js";import{e as S,l as k,o,c as p,h as f,b as s,w as d,aL as w,O as r,a as u}from"./DFVm4NPh.js";const B=["innerHTML"],C={class:"p-4"},z=S({__name:"ModalInputForm",props:{isOnlyShowIcon:{type:Boolean,required:!1,default:!1},buttonLabel:{type:String,default:"Show"},outlined:{type:Boolean,required:!1,default:!1},buttonIcon:{type:String,default:"pi pi-eye"},buttonSeverity:{type:String,default:"contrast"},header:{type:String,default:"Form"},width:{type:String,default:"40rem"},maximizable:{type:Boolean,default:!1},breakpoints:{type:Object,default:()=>({"1199px":"75vw","575px":"90vw"})},confirmLabel:{type:String,default:"Confirm"},cancelLabel:{type:String,default:"Cancel"},disabled:{type:Boolean,default:!1}},emits:["on-confirm","on-blinding-insert","on-blinding-update"],setup(e,{emit:m}){const i=m,n=k(!1),b=()=>{n.value=!0,i("on-blinding-update"),i("on-blinding-insert")},y=()=>{i("on-confirm"),n.value=!1};return(a,t)=>{const l=h,v=g;return o(),p("div",null,[e.isOnlyShowIcon?(o(),f(l,{key:0,icon:e.buttonIcon,outlined:"",rounded:"",severity:e.buttonSeverity,onClick:b},null,8,["icon","severity"])):(o(),f(l,{key:1,label:e.buttonLabel,onClick:b,disabled:e.disabled,outlined:e.outlined,icon:e.buttonIcon,severity:e.buttonSeverity},null,8,["label","disabled","outlined","icon","severity"])),s(v,{visible:n.value,"onUpdate:visible":t[1]||(t[1]=c=>n.value=c),header:e.header,style:w({width:e.width}),breakpoints:e.breakpoints,maximizable:e.maximizable,modal:"",dismissableMask:"",appendTo:"body"},{header:d(()=>[r(a.$slots,"header",{},()=>[u("h2",{class:"font-bold",innerHTML:e.header},null,8,B)])]),footer:d(()=>[r(a.$slots,"footer",{},()=>[s(l,{label:e.cancelLabel,text:"",severity:"secondary",onClick:t[0]||(t[0]=c=>n.value=!1)},null,8,["label"]),s(l,{label:e.confirmLabel,outlined:"",severity:"primary",onClick:y},null,8,["label"])])]),default:d(()=>[u("div",C,[r(a.$slots,"body",{},()=>[t[2]||(t[2]=u("p",{class:"text-gray-500"},"Default content here. Override this by using slot.",-1))])])]),_:3},8,["visible","header","style","breakpoints","maximizable"])])}}});export{z as _};
