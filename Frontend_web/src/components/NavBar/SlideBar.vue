<template>
  <!-- drawer init and show -->
  <div class="text-center">
    <button
      class="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800"
      type="button"
      data-drawer-target="drawer-navigation"
      data-drawer-show="drawer-navigation"
      aria-controls="drawer-navigation"
    >
      Show navigation
    </button>
  </div>

  <!-- drawer component -->
  <div
    id="drawer-navigation"
    class="fixed top-0 left-0 z-40 w-[500px] h-screen p-4 overflow-y-auto transition-transform -translate-x-full bg-white dark:bg-gray-800"
    tabindex="-1"
    aria-labelledby="drawer-navigation-label"
  >
    <h5 id="drawer-navigation-label" class="text-base font-semibold text-gray-500 uppercase dark:text-gray-400">Menu</h5>
    <div className="flex justify-between items-center mb-4">
      <h2 className="text-lg font-bold">Filters</h2>
      <button className="text-gray-500 hover:text-red-500">✕</button>
    </div>

    {/* Product Brand and Model */}
    <div className="mb-4">
      <label className="block text-sm font-medium mb-1">Product Brand</label>
      <select className="w-full p-2 border rounded">
        <option>Samsung</option>
        <option>Apple</option>
        <option>LG</option>
      </select>
    </div>
    <div className="mb-4">
      <label className="block text-sm font-medium mb-1">Product Model</label>
      <select className="w-full p-2 border rounded">
        <option>iMac 27"</option>
        <option>MacBook Pro</option>
        <option>Galaxy S21</option>
      </select>
    </div>

    {/* Manufacture Year */}
    <div className="mb-4">
      <label className="block text-sm font-medium mb-1">Manufacture Year</label>
      <div className="flex gap-2">
        <input type="date" className="w-1/2 p-2 border rounded" />
        <input type="date" className="w-1/2 p-2 border rounded" />
      </div>
    </div>

    {/* Price Range */}
    <div className="mb-4">
      <label className="block text-sm font-medium mb-1">Price Range</label>
      <div className="flex justify-between text-sm">
        <span>300</span>
        <span>3500</span>
      </div>
      <input type="range" className="w-full" min="300" max="3500" />
      <div className="flex justify-between mt-2">
        <input type="number" className="w-1/2 p-2 border rounded" placeholder="From" />
        <input type="number" className="w-1/2 p-2 border rounded" placeholder="To" />
      </div>
    </div>

    {/* Condition */}
    <div className="mb-4">
      <label className="block text-sm font-medium mb-1">Condition</label>
      <div className="flex gap-2">
        <label> <input type="radio" name="condition" className="mr-1" /> All </label>
        <label> <input type="radio" name="condition" className="mr-1" /> New </label>
        <label> <input type="radio" name="condition" className="mr-1" /> Used </label>
      </div>
    </div>

    {/* Colour */}
    <div className="mb-4">
      <label className="block text-sm font-medium mb-1">Colour</label>
      <div className="flex gap-2">
        <label> <input type="checkbox" className="mr-1" /> Blue </label>
        <label> <input type="checkbox" className="mr-1" /> Gray </label>
        <label> <input type="checkbox" className="mr-1" checked /> Green </label>
        <label> <input type="checkbox" className="mr-1" /> Pink </label>
        <label> <input type="checkbox" className="mr-1" /> Red </label>
      </div>
    </div>

    {/* Rating */}
    <div className="mb-4">
      <label className="block text-sm font-medium mb-1">Rating</label>
      <div className="flex items-center gap-2">
        {[5, 4, 3, 2, 1].map((star) => (
        <label key="{star}" className="flex items-center">
          <input type="radio" name="rating" className="mr-1" />
          {'★'.repeat(star)}{'☆'.repeat(5 - star)}
        </label>
        ))}
      </div>
    </div>

    {/* Delivery */}
    <div className="mb-4">
      <label className="block text-sm font-medium mb-1">Delivery</label>
      <div className="grid grid-cols-2 gap-2">
        <button className="p-2 border rounded text-center">USA</button>
        <button className="p-2 border rounded text-center">Europe</button>
        <button className="p-2 border rounded text-center">Asia</button>
        <button className="p-2 border rounded text-center">Australia</button>
      </div>
    </div>

    {/* Action Buttons */}
    <div className="flex gap-2">
      <button className="w-1/2 bg-blue-500 text-white p-2 rounded">Apply filters</button>
      <button className="w-1/2 bg-gray-300 p-2 rounded">Clear all</button>
    </div>
  </div>
</template>
