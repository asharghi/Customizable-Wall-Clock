<template>
  <div class="container">
    <div class="empty-settings-row"></div>
    <SliderSetting
      label="Størrelse"
      :min="500"
      :max="2000"
      :modelValue="localSettings.size"
      @change="updateValue('size', parseInt($event))"
    />
    <div class="empty-settings-row"></div>
    <BoolSetting
      label="Bruk bakgrunnsbilde"
      :modelValue="localSettings.useimage"
      @change="updateValue('useimage', $event)"
    />
    <div class="empty-settings-row"></div>
    <SliderSetting
      label="Farge"
      :min="0"
      :max="360"
      :modelValue="localSettings.hue"
      :hideNumberValue="true"
      :disabled="localSettings.useimage"
      @change="updateValue('hue', parseInt($event))"
      sliderClass="color-picker"
    />
    <SliderSetting
      label="Lysstyrke"
      :min="0"
      :max="100"
      :modelValue="localSettings.luminosity"
      :hideNumberValue="true"
      :disabled="localSettings.useimage"
      @change="updateValue('luminosity', parseInt($event))"
      sliderClass="darkness-picker"
    />
  </div>
</template>

<script>
import SliderSetting from "./SliderSetting";
import BoolSetting from "./BoolSetting";
export default {
  components: {
    SliderSetting,
    BoolSetting
  },
  props: {
    settings: {
      type: Object,
      default: () => {
        return {
          size: 700,
          useimage: false,
          hue: 50,
          luminosity: 50
        };
      }
    }
  },
  data() {
    return {
      localSettings: this.settings
    };
  },
  methods: {
    updateValue(dataPropertyName, value) {
      this.localSettings[dataPropertyName] = value;
      this.$emit("change", this.localSettings);
    }
  },
  watch: {
    settings: {
      handler(newValue) {
        this.localSettings = newValue;
      },
      deep: true
    }
  }
};
</script>
<style scoped>
.container {
  display: inline-block;
  width: 100%;
  position: absolute;
}
</style>