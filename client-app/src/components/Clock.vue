<template>
  <div class="clock-face center" :style="clockStyle">
    <div
      v-for="n in 60"
      :key="n+'-dot'"
      class="dot-container center"
      :style="'transform: translate(-50%, -50%) rotate(' + (n * 6) + 'deg)'"
    >
      <div :style="styleString(n)"></div>
    </div>
    <div
      v-for="n in 12"
      :key="n+'-font'"
      class="dot-container center"
      :style="'transform: translate(-50%, -50%) rotate(' + (n * 30) + 'deg)'"
    >
      <div :style="fontStyle(n)">{{fontSettings(n).fontface === 'Geek' ? romanNumber(n) : n}}</div>
    </div>
    <div class="center single-pixel">
      <div :style="minutePointerStyle"></div>
      <div :style="hourPointerStyle"></div>
      <div :style="secondPointerStyle"></div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      secondPointerStyle: "",
      minutePointerStyle: "",
      hourPointerStyle: ""
    };
  },
  props: {
    dotMinutesSettings: Object,
    dotFifthSettings: Object,
    dotQuarterSettings: Object,
    secondPointerSettings: Object,
    minutePointerSettings: Object,
    hourPointerSettings: Object,
    fontHourSettings: Object,
    fontQuarterSettings: Object,
    watchFaceSettings: Object
  },
  computed: {
    clockStyle() {
      const background =
        this.watchFaceSettings.useimage && this.watchFaceSettings.imagepath
          ? "url(" + this.watchFaceSettings.imagepath + ");"
          : "hsl(" +
            this.watchFaceSettings.hue +
            ", 100%, " +
            this.watchFaceSettings.luminosity +
            "%);";
      return (
        "background: " +
        background +
        "width: " +
        this.watchFaceSettings.size +
        "px;height: " +
        this.watchFaceSettings.size +
        "px;"
      );
    }
  },
  mounted() {
    setInterval(this.updateTime, 1000);
  },
  methods: {
    romanNumber(n) {
      const map = {
        1: "1",
        2: "11",
        3: "111",
        4: "1V",
        5: "V",
        6: "V1",
        7: "V11",
        8: "V111",
        9: "1X",
        10: "X",
        11: "X1",
        12: "X11"
      };
      return map[n];
    },
    updateTime() {
      const comp = this;
      const now = new Date();
      const seconds = now.getSeconds();
      const minutes = now.getMinutes();
      const hours = now.getHours();

      comp.secondPointerStyle = comp.pointerStyle(
        comp.secondPointerSettings,
        seconds
      );
      comp.minutePointerStyle = comp.pointerStyle(
        comp.minutePointerSettings,
        minutes
      );
      comp.hourPointerStyle = comp.pointerStyle(
        comp.hourPointerSettings,
        (hours * 5) + ((minutes/60) * 5)
      );
    },
    fontSettings(n) {
      if (
        !(n % 3) &&
        !!this.fontQuarterSettings &&
        this.fontQuarterSettings.active
      )
        return this.fontQuarterSettings;

      if (this.fontHourSettings && this.fontHourSettings.active)
        return this.fontHourSettings;

      return {
        active: false,
        size: 0,
        hue: 360,
        luminosity: 50,
        space: 0
      };
    },
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
    fontStyle(n) {
      const fontSettings = this.fontSettings(n);
      return (
        "font-family: " +
        fontSettings.fontface +
        "; margin-top: " +
        fontSettings.space +
        "px; font-size: " +
        fontSettings.size +
        "px; transform: rotate(-" +
        n * 30 +
        "deg);color: hsl(" +
        fontSettings.hue +
        ", 100%, " +
        fontSettings.luminosity +
        "%);"
      );
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
    },
    pointerStyle(pointerSetting, time) {
      return pointerSetting.active
        ? "position: absolute;left: 0px;top: 0px;transform-origin: center left;transform: translate(0, -50%) rotate(" +
            ((time / 60) * 360 - 90) +
            "deg);width:" +
            pointerSetting.width +
            "px;height:" +
            pointerSetting.height +
            "px;border-radius:" +
            pointerSetting.radius +
            "px;background: hsl(" +
            pointerSetting.hue +
            ", 100%, " +
            pointerSetting.luminosity +
            "%);"
        : "";
    }
  },
  watch: {
    secondPointerSettings: {
      handler() {
        this.updateTime();
      },
      deep: true
    },
    minutePointerSettings: {
      handler() {
        this.updateTime();
      },
      deep: true
    },
    hourPointerSettings: {
      handler() {
        this.updateTime();
      },
      deep: true
    }
  }
};
</script>

<style scoped>
.clock-face {
  border-radius: 50%;
}
.center {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
}
.single-pixel {
  width: 1px;
  height: 1px;
}
.dot-container {
  height: 100%;
}
</style>
