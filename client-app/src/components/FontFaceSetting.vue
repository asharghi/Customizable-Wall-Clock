<template>
  <div>
    <div class="settings-row" v-for="font in fonts" v-key="font" @click="$emit('selected', font)">
      <span class="settings-font">{{ font + ' Font'}}</span>
      <span :style="'font-size:24px; font-family: ' + font">1 2 3 4 5 6 7 8 9 10 11 12</span>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      fonts: [
        "Abel",
        "AbrilFatface",
        "Anton",
        "Bitter",
        "CormorantGaramond",
        "Geek",
        "Kalam",
        "Lobster",
        "Monoton",
        "OldStandardTT",
        "Orbitron",
        "PTSans"
      ]
    };
  }
};
</script>