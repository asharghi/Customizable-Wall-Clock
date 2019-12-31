<template>
  <div class="container">
    <div class="empty-settings-row"></div>
    <BoolSetting
      label="Aktiver"
      :modelValue="localSettings.active"
      @change="updateValue('active', $event)"
    />
    <div class="empty-settings-row"></div>
    <SliderSetting
      label="Høyde"
      :min="1"
      :max="1000"
      :modelValue="localSettings.width"
      :disabled="!localSettings.active"
      @change="updateValue('width', parseInt($event))"
    />
    <SliderSetting
      label="Bredde"
      :min="1"
      :max="50"
      :modelValue="localSettings.height"
      :disabled="!localSettings.active"
      @change="updateValue('height', parseInt($event))"
    />
    <SliderSetting
      label="Radius"
      :min="0"
      :max="20"
      :modelValue="localSettings.radius"
      :disabled="!localSettings.active"
      @change="updateValue('radius', parseInt($event))"
    />
    <div class="empty-settings-row"></div>
    <SliderSetting
      label="Farge"
      :min="0"
      :max="360"
      :modelValue="localSettings.hue"
      :disabled="!localSettings.active"
      @change="updateValue('hue', parseInt($event))"
      sliderClass="color-picker"
    />
    <SliderSetting
      label="Lysstyrke"
      :min="0"
      :max="100"
      :modelValue="localSettings.luminosity"
      :disabled="!localSettings.active"
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
          width: 5,
          height: 5,
          radius: 5,
          hue: 50,
          luminosity: 50,
          active: false
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