import{_ as w}from"./nzhyw4-Z.js";import{_ as c}from"./BnmkgdtR.js";import{u as _,_ as y}from"./CwrryA99.js";import{X as s}from"./BDEeDIwr.js";import{e as k,k as N,l as v,s as C,f as P,g as U,o as r,h as q,w as B,c as n,a as e,d as V,n as $,j as p,i as l,b as i}from"./DFVm4NPh.js";import{_ as I}from"./Ddd57dIF.js";import{_ as S}from"./Bfgw1-s2.js";import"./awSMyq0J.js";import"./DlAUqK2U.js";import"./Bj-PnIja.js";import"./BpXJ3t4k.js";import"./B05HPIib.js";import"./D5y8FroI.js";import"./B_7wH41M.js";import"./QbBb9rFs.js";import"./B1VAvD7b.js";import"./5Q-RKaCz.js";import"./BCNAepu4.js";import"./0qAV6zC5.js";import"./DgmwlLno.js";import"./Bzy3NznM.js";import"./h9XeG6ng.js";import"./V8n92ecy.js";import"./DPxmNygB.js";import"./DsIA2G5F.js";import"./BKCMpDB6.js";import"./PLEq0hNF.js";import"./B33bflHF.js";import"./DxTMCm_i.js";import"./Bug8ffv2.js";import"./DqbQgBaR.js";import"./BuMp5kCj.js";const j={id:"gradient-card",class:"animate-fade-left animate-ease-out animate-delay-100 p-10 md:max-w-none md:p-12 lg:p-30 rounded-xl mb-16 max-w-screen-lg mx-auto h-auto overflow-hidden",ref:"gradientCard"},z={class:"absolute inset-0 pointer-events-none z-0 gradient-effect",ref:"gradientEffect"},D={class:"animate-fade-right grid grid-cols-1 md:grid-cols-2 xl:grid-cols-3 gap-8"},E={class:"space-y-4 xl:col-span-1"},X={class:"w-full"},M=["src"],T={key:1,class:"rounded-full object-cover mx-auto aspect-square w-40 h-40 md:w-64 md:h-64",src:"https://static.vecteezy.com/system/resources/previews/009/292/244/non_2x/default-avatar-icon-of-social-media-user-vector.jpg",alt:"User Profile Image"},A={class:"space-y-4 xl:col-span-2"},F={class:"flex space-x-4"},ke=k({__name:"UserProfile",setup(G){const g=_(),d=N(),a=v(!1),u=v(!1),{fieldValues:f,fieldErrors:o}=C(d),x=P({initialValues:f.value});d.SetFields(x);const m={email:s({id:"email",name:"Email",rules:"required",visible:!0,option:""}),phoneNumber:s({id:"phoneNumber",name:"Phone Number",rules:"required",visible:!0,option:""}),birthday:s({id:"birthday",name:"Birthday",rules:"",visible:!0,option:""}),firstName:s({id:"firstName",name:"First Name",rules:"required",visible:!0,option:""}),birthDate:s({id:"birthDate",name:"birth Date",rules:"required",visible:!0,option:""}),lastName:s({id:"lastName",name:"Last Name",rules:"required",visible:!0,option:""}),gender:s({id:"gender",name:"Gender",rules:"required",visible:!0,option:""})},b=async()=>{await d.UpdateUser(),a.value=!1,u.value=!0,setTimeout(()=>{u.value=!1},3e3)};return U(async()=>{await d.GetProfile()}),(Z,t)=>(r(),q(w,null,{body:B(()=>[u.value?(r(),n("div",{key:0,class:$(["fixed top-5 right-5 z-50 w-96 transition-opacity duration-1000 ease-out",{"opacity-0":!u.value}])},t[2]||(t[2]=[e("div",{class:"flex items-center p-4 text-sm text-green-800 rounded-lg bg-green-50 shadow-lg",role:"alert"},[e("svg",{class:"flex-shrink-0 w-4 h-4 mr-3","aria-hidden":"true",xmlns:"http://www.w3.org/2000/svg",fill:"currentColor",viewBox:"0 0 20 20"},[e("path",{d:"M18 10A8 8 0 1 1 2 10a8 8 0 0 1 16 0ZM9 13h2v2H9v-2Zm0-6h2v4H9V7Z"})]),e("span",{class:"font-medium"},"Success!"),V(" Profile updated successfully. ")],-1)]),2)):p("",!0),e("div",j,[e("div",z,null,512),t[5]||(t[5]=e("div",{class:"max-w-3xl mx-auto text-center relative z-10 mt-0 pt-0"},[e("h1",{class:"text-4xl font-bold text-black dark:text-white mb-4 animate-fade-up animate-duration-1000 animate-delay-500"},"User Profile"),e("p",{class:"text-lg text-gray-700 mb-8 animate-fade-up animate-duration-1000 animate-delay-500"},"Edit Your Own Personal Informations!"),e("div"),e("div",{class:"mt-8 flex justify-center space-x-4"})],-1)),e("div",D,[e("div",E,[t[3]||(t[3]=e("h2",{class:"text-2xl text-black dark:text-white font-semibold"},"Profile",-1)),t[4]||(t[4]=e("p",{class:"text-sm text-black dark:text-white"},"This information will be displayed publicly so be careful what you share.",-1)),e("div",X,[l(g).uploadImage.length>0?(r(),n("img",{key:0,class:"rounded-full object-cover mx-auto aspect-square w-40 h-40 md:w-64 md:h-64",src:l(g).uploadImage[0].imagePreview,alt:"image description"},null,8,M)):(r(),n("img",T))])]),e("div",A,[e("div",null,[i(c,{"xml-column":m.email,maxlength:50,disabled:!0,"err-msg":l(o).email,placeholder:"abc@gmail.com"},null,8,["xml-column","err-msg"])]),e("div",null,[i(c,{"xml-column":m.firstName,maxlength:50,disabled:!a.value,"err-msg":l(o).firstName,placeholder:"John"},null,8,["xml-column","disabled","err-msg"])]),e("div",null,[i(c,{"xml-column":m.lastName,maxlength:50,disabled:!a.value,"err-msg":l(o).lastName,placeholder:"John"},null,8,["xml-column","disabled","err-msg"])]),e("div",null,[i(I,{"xml-column":m.birthDate,"err-msg":l(o).birthDate,disabled:!a.value,maxlength:50,"date-model":"11/1/2002","is-inline":!1,"date-picker-position":"bottom right"},null,8,["xml-column","err-msg","disabled"])]),e("div",null,[i(c,{"xml-column":m.phoneNumber,maxlength:50,disabled:!a.value,"err-msg":l(o).phoneNumber,placeholder:"+84XXX"},null,8,["xml-column","disabled","err-msg"])]),e("div",null,[i(S,{"xml-column":m.gender,model:l(f).gender,disabled:!a.value,"err-msg":l(o).gender,"master-name":"Gender"},null,8,["xml-column","model","disabled","err-msg"])]),e("div",null,[i(y,{"max-number-image":1,"is-show-popover":!1,label:"Upload avatar image"})]),e("div",F,[a.value?(r(),n("button",{key:0,onClick:t[0]||(t[0]=h=>a.value=!1),class:"w-full bg-gray-500 text-white p-3 rounded-lg hover:bg-gray-600"},"Back")):p("",!0),a.value?(r(),n("button",{key:1,onClick:b,class:"w-full bg-green-500 text-white p-3 rounded-lg hover:bg-green-600"},"Save Changes")):p("",!0),a.value?p("",!0):(r(),n("button",{key:2,onClick:t[1]||(t[1]=h=>a.value=!0),class:"w-full bg-blue-500 text-white p-3 rounded-lg hover:bg-blue-600"}," Update Account "))])])])],512)]),_:1}))}});export{ke as default};
