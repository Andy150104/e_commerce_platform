import{e as i,dn as a,dp as d,D as n,dq as c,dr as f,o as b,c as g,O as p,d as v,t as m,n as y}from"./DFVm4NPh.js";import{_ as C}from"./DlAUqK2U.js";const O=i({props:{size:{type:String,default:()=>a.ui.badge.default.size,validator(e){return Object.keys(a.ui.badge.size).includes(e)}},color:{type:String,default:()=>a.ui.badge.default.color,validator(e){return[...a.ui.colors,...Object.keys(a.ui.badge.color)].includes(e)}},variant:{type:String,default:()=>a.ui.badge.default.variant,validator(e){return[...Object.keys(a.ui.badge.variant),...Object.values(a.ui.badge.color).flatMap(s=>Object.keys(s))].includes(e)}},label:{type:String,default:null},ui:{type:Object,default:()=>a.ui.badge}},setup(e){const s=d(),t=n(()=>c({},e.ui,s.ui.badge));return{badgeClass:n(()=>{var o,r;const l=((r=(o=t.value.color)==null?void 0:o[e.color])==null?void 0:r[e.variant])||t.value.variant[e.variant];return f(t.value.base,t.value.font,t.value.rounded,t.value.size[e.size],l==null?void 0:l.replaceAll("{color}",e.color))})}}});function j(e,s,t,u,l,o){return b(),g("span",{class:y(e.badgeClass)},[p(e.$slots,"default",{},()=>[v(m(e.label),1)])],2)}const S=C(O,[["render",j]]);export{S as default};
