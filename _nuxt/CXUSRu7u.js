import{_ as c,C as p}from"./DGXY1j6i.js";import{_ as l}from"./nzhyw4-Z.js";import d from"./Ck2GeduC.js";import{u as f}from"./7cbLggmU.js";import{i as h}from"./B1VAvD7b.js";import{e as _,g as w,o as r,h as s,w as i,a as y,b as a,i as n,j as b,m as C}from"./DFVm4NPh.js";import"./CMS-NkLX.js";import"./Cgecvcl9.js";import"./DlAUqK2U.js";import"./awSMyq0J.js";import"./Bj-PnIja.js";import"./BpXJ3t4k.js";import"./B05HPIib.js";import"./D5y8FroI.js";import"./B_7wH41M.js";import"./QbBb9rFs.js";import"./BCNAepu4.js";import"./0qAV6zC5.js";import"./5Q-RKaCz.js";const q=_({__name:"Wishlist",setup(x){const t=f(),m=async o=>{C().push(`/Service/Buying/Product/${o}`)},u=async o=>{await t.RemoveItemOutWishlist(o.codeProduct),await t.GetProductWishlist()};return w(async()=>{await t.GetProductWishlist(),h()}),(o,e)=>(r(),s(l,null,{body:i(()=>[e[0]||(e[0]=y("h2",{class:"text-2xl font-bold text-center text-gray-900 mb-8 dark:text-white"},"Your Wishlist",-1)),a(d,null,{body:i(()=>[a(c,{ref:"cardRef","is-show-wish-list":!1,"is-show-button2":!0,"product-model":n(t).produtList,"label-name":"View",onOnFunctionBut2:u,onOnBuy:m,"is-have-side-bar":!0,"button2-name":"Remove Wishlist"},null,8,["product-model"]),n(t).isLoadingSkeletonCard?(r(),s(p,{key:0})):b("",!0)]),_:1})]),_:1}))}});export{q as default};
