import{e as m,l,g as f,L as p,K as x,o as _,c as g,a as t,O as y,n as i,i as c,A as d}from"./DFVm4NPh.js";import{_ as b}from"./DlAUqK2U.js";const E={class:"relative"},w={class:"max-w-3xl mx-auto text-center relative z-10"},T={class:"mt-8 flex justify-center space-x-4"},h=m({__name:"GradientCard",setup(C){const s=l(null),o=l(null),n=a=>{const e=s.value.getBoundingClientRect(),u=a.clientX-e.left,v=a.clientY-e.top;o.value.style.transform=`translate(${u-500}px, ${v-500}px)`,o.value.style.opacity="1"},r=()=>{o.value.style.opacity="0"};return f(async()=>{await p(),s.value&&o.value&&(s.value.addEventListener("mousemove",n),s.value.addEventListener("mouseleave",r))}),x(()=>{var a,e;(a=s.value)==null||a.removeEventListener("mousemove",n),(e=s.value)==null||e.removeEventListener("mouseleave",r)}),(a,e)=>(_(),g("div",E,[t("div",{id:"gradient-card",class:"bg-gradient-to-r from-blue-100 via-blue-200 to-purple-100 p-8 md:max-w-none md:p-16 lg:p-48 rounded-xl mb-16 max-w-screen-lg mx-auto h-auto overflow-hidden",ref_key:"gradientCard",ref:s},[t("div",{class:"absolute inset-0 pointer-events-none z-0 gradient-effect",ref_key:"gradientEffect",ref:o},null,512),t("div",w,[e[0]||(e[0]=t("h1",{class:"text-4xl font-bold text-gray-900 mb-4"},"Discover, Trade, and Collect Your Unique Accessories",-1)),e[1]||(e[1]=t("p",{class:"text-lg text-gray-700 mb-8"}," Unlock a world of surprises with our exclusive blind boxes. Trade and collect accessories effortlessly through our seamless platform ",-1)),t("div",null,[y(a.$slots,"search",{},void 0,!0)]),t("div",T,[t("button",{class:i(c(d).BUTTON_GRADIENT_1)},"Buy Now",2),t("button",{class:i(c(d).BUTTON_GRADIENT_2)},"Exchange now",2)])])],512)]))}}),k=b(h,[["__scopeId","data-v-bfc6ea5e"]]);export{k as default};
