<template>
  <header>
    <div
      class="grid grid-cols-3 items-center place-items-center bg-orange-300 rounded-b-xl py-1 select-none"
    >
      <div></div>
      <div class="font-bold text-3xl">Простые финансы</div>
      <div class="flex justify-end w-11/12 *:ml-2">
        <RouterLink to="/">
          <div class="font-bold">Home</div>
        </RouterLink>
      </div>
    </div>
    <!-- <div class="text-center font-bold text-3xl bg-orange-300 rounded-b-xl py-1 select-none">
      Простые финансы
    </div> -->
  </header>

  <div class="container mx-auto">
    <RouterView />
  </div>
</template>
