<template>
  <div class="container">
    <template v-if="!localSettings.userolex">
      <div class="empty-settings-row"></div>
      <SliderSetting
        label="Størrelse"
        :min="500"
        :max="2000"
        :modelValue="localSettings.size"
        @change="updateValue('size', parseInt($event))"
      />
      <div class="empty-settings-row"></div>
      <SliderSetting
        label="Horisontal posisjon"
        :min="-150"
        :max="150"
        :modelValue="localSettings.left"
        @change="updateValue('left', parseInt($event))"
      />
      <SliderSetting
        label="Vertikal posisjon"
        :min="-150"
        :max="150"
        :modelValue="localSettings.top"
        @change="updateValue('top', parseInt($event))"
      />
      <div class="empty-settings-row"></div>
      <BoolSetting
        label="Bruk bakgrunnsbilde"
        :modelValue="localSettings.useimage"
        @change="updateValue('useimage', $event)"
      />
      <div class="empty-settings-row"></div>
    </template>
    <div v-if="localSettings.useimage && !localSettings.userolex">
      <BackgroundSetting
        @selected="updateValue('imagepath', $event)"
        :selectedPath="localSettings.imagepath"
      />
    </div>
    <div v-if="!localSettings.useimage && !localSettings.userolex">
      <SliderSetting
        label="Farge"
        :min="0"
        :max="360"
        :modelValue="localSettings.hue"
        @change="updateValue('hue', parseInt($event))"
        sliderClass="color-picker"
      />
      <SliderSetting
        label="Lysstyrke"
        :min="0"
        :max="100"
        :modelValue="localSettings.luminosity"
        @change="updateValue('luminosity', parseInt($event))"
        sliderClass="darkness-picker"
      />
    </div>
    <div class="empty-settings-row"></div>
    <BoolSetting
      label="Bruk Rolex"
      :modelValue="localSettings.userolex"
      @change="updateValue('userolex', $event)"
    />
    <template v-if="localSettings.userolex">
      <div class="empty-settings-row"></div>
      <SliderSetting
        label="Rolex størrelse"
        :min="1"
        :max="300"
        :modelValue="localSettings.rolexsize"
        @change="updateValue('rolexsize', parseInt($event))"
      />
      <SliderSetting
        label="Rolex horisontal posisjon"
        :min="-900"
        :max="100"
        :modelValue="localSettings.rolexleft"
        @change="updateValue('rolexleft', parseInt($event))"
      />
      <SliderSetting
        label="Rolex vertikal posisjon"
        :min="-900"
        :max="100"
        :modelValue="localSettings.rolextop"
        @change="updateValue('rolextop', parseInt($event))"
      />
    </template>
  </div>
</template>

<script>
import SliderSetting from "./SliderSetting";
import BoolSetting from "./BoolSetting";
import BackgroundSetting from "./BackgroundSetting";
export default {
  components: {
    SliderSetting,
    BoolSetting,
    BackgroundSetting
  },
  props: {
    settings: {
      type: Object,
      default: () => {
        return {
          size: 500,
          left: 0,
          top: 0,
          useimage: false,
          userolex: false,
          rolexleft: 0,
          rolextop: 0,
          rolexsize: 100,
          hue: 50,
          luminosity: 50,
          imagepath: ""
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