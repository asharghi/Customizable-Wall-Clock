<template>
  <div class="clock-face" :style="clockStyle">
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
    watchFaceSettings: Object
  },
  computed: {
    clockStyle() {
      return (
        "background: hsl(" +
        this.watchFaceSettings.hue +
        ", 100%, " +
        this.watchFaceSettings.luminosity +
        "%);" +
        "width: " +
        this.watchFaceSettings.size +
        "px;height: " +
        this.watchFaceSettings.size +
        "px;"
      );
    }
  },
  methods: {
    dotSettings(n) {
      if (
        !((n * 6) % 90) &&
        !!this.dotQuarterSettings &&
        this.dotQuarterSettings.active
      )
        return this.dotQuarterSettings;
      if (
        !((n * 6) % 30) &&
        !!this.dotFifthSettings &&
        this.dotFifthSettings.active
      )
        return this.dotFifthSettings;
      if (!!this.dotMinutesSettings && this.dotMinutesSettings.active)
        return this.dotMinutesSettings;
      return {
        space: 0,
        width: 0,
        height: 0,
        radius: 0,
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
