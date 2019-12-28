<template>
  <div
    class="clock-face"
    :style="'background-color:'+ backgroundImage + ';width: ' + size + 'px;height: ' + size + 'px;'"
  >
    <div
      v-for="n in 60"
      :key="n"
      class="dot-container"
      :style="'transform: translate(-50%, -50%) rotate(' + (n * 6) + 'deg)'"
    >
      <div :style="styleString(n)"></div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    dotMinutesSettings: Object,
    dotFifthSettings: Object,
    dotQuarterSettings: Object,
    backgroundImage: String,
    size: Number
  },
  methods: {
    dotSettings(n) {
      if (!((n * 6) % 90) && !!this.dotQuarterSettings)
        return this.dotQuarterSettings;
      if (!((n * 6) % 30) && !!this.dotFifthSettings)
        return this.dotFifthSettings;
      if (!!this.dotMinutesSettings) return this.dotMinutesSettings;
      return {
        space: 20,
        width: 5,
        height: 5,
        radius: 5,
        hue: 50,
        luminosity: 50
      };
    },
    styleString(n) {
      const dotSettings = this.dotSettings(n);
      return (
        "margin-top: " +
        dotSettings.space +
        "px;width: " +
        dotSettings.width +
        "px;height: " +
        dotSettings.height +
        "px;background: hsl(" +
        dotSettings.hue +
        ", 100%, " +
        dotSettings.luminosity +
        "%);border-radius: " +
        dotSettings.radius +
        "px;"
      );
    }
  }
};
</script>

<style scoped>
.clock-face {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  border-radius: 50%;
}
.dot-container {
  height: 100%;
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
}
</style>
