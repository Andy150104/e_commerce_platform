import{e as n,dn as s,dp as u,D as o,dq as t,o as d,c as r,O as p,d as l,t as f,n as c}from"./DFVm4NPh.js";import{_ as m}from"./DlAUqK2U.js";const b=n({props:{value:{type:String,default:null},size:{type:String,default:()=>s.ui.kbd.default.size,validator(e){return Object.keys(s.ui.kbd.size).includes(e)}},ui:{type:Object,default:()=>s.ui.kbd}},setup(e){const a=u();return{ui:o(()=>t({},e.ui,a.ui.kbd))}}});function g(e,a,i,k,z,y){return d(),r("kbd",{class:c([e.ui.base,e.ui.size[e.size],e.ui.padding,e.ui.rounded,e.ui.font,e.ui.background,e.ui.ring])},[p(e.$slots,"default",{},()=>[l(f(e.value),1)])],2)}const S=m(b,[["render",g]]);export{S as default};
