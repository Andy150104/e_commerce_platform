import{u as q}from"./DgmwlLno.js";import{e as g,l as f,g as C,af as B,L as y,o as l,c as s,F as b,r as k,b as n,i as d,b0 as S,a as h,t as v,b6 as N}from"./DFVm4NPh.js";import{_ as w}from"./Bzy3NznM.js";const _={class:"flex"},j=["value"],F=g({__name:"BaseControlRadioButton",props:{model:{type:[String,Number],required:!0},xmlColumn:{type:Object,required:!0},masterName:{type:String,required:!0},errMsg:{type:String,default:""},params:{type:Object,required:!1,default:()=>({})},disabled:{type:Boolean,required:!0},move:{type:Boolean,required:!1,default:!0},formClass:{type:String,required:!1,default:""},isShowError:{type:Boolean,required:!1,default:!0}},setup(e,{expose:m}){const a=e,o=f([]),i=q.v4(),p=f(a.xmlColumn.rules),c=async()=>{await y(),o.value=await N(a.masterName,{xmlColumn:a.xmlColumn,...a.params})},x=()=>{document.getElementById(i).focus()};return C(async()=>{B(a,t=>{var u;p.value=(u=t==null?void 0:t.xmlColumn)==null?void 0:u.rules}),await y(),await c()}),m({fetch:c,onFocus:x}),(t,u)=>(l(),s("div",_,[(l(!0),s(b,null,k(d(o),r=>(l(),s("div",{key:r.value,class:"flex items-center me-4"},[n(d(S),{class:"w-6 h-6 text-blue-600 bg-gray-100 border-gray-300 focus:ring-blue-500 dark:focus:ring-blue-600 dark:ring-offset-gray-800 focus:ring-2 dark:bg-gray-700 dark:border-gray-600",id:d(i)+r.value,name:e.xmlColumn.id,type:"radio",value:r.value,checked:r.checked,disabled:e.disabled},null,8,["id","name","value","checked","disabled"]),h("label",{value:r.value,class:"ms-2 text-sm font-medium text-gray-900 dark:text-gray-300"},v(r.label),9,j)]))),128))]))}}),M=g({__name:"UserControlRadioButton",props:{model:{type:[String,Number],required:!0},xmlColumn:{type:Object,required:!0},errMsg:{type:String,required:!0,default:""},disabled:{type:Boolean,required:!0},move:{type:Boolean,required:!1,default:!0},masterName:{type:String,required:!0},params:{type:Object,required:!1,default:()=>({})},labelClass:{type:String,required:!1,default:""},formClass:{type:String,required:!1,default:""}},setup(e){return(m,a)=>(l(),s(b,null,[n(w,{"xml-column":e.xmlColumn},null,8,["xml-column"]),n(F,{ref:"radioButtonRef",model:e.model,"xml-column":e.xmlColumn,disabled:e.disabled,"master-name":e.masterName,params:e.params},null,8,["model","xml-column","disabled","master-name","params"])],64))}});export{M as _};
