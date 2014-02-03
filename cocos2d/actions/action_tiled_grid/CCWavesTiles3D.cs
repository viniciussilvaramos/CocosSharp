/****************************************************************************
Copyright (c) 2010-2012 cocos2d-x.org
Copyright (c) 2008-2010 Ricardo Quesada
Copyright (c) 2011 Zynga Inc.
Copyright (c) 2011-2012 openxlive.com
 
Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
****************************************************************************/

using System;

namespace CocosSharp
{
    public class CCWavesTiles3D : CCTiledGrid3DAction
    {
        protected float m_fAmplitude;
        protected float m_fAmplitudeRate;
        protected int m_nWaves;

        /// <summary>
        /// waves amplitude 
        /// </summary>
        /// <returns></returns>
        public float Amplitude
        {
            get { return m_fAmplitude; }
            set { m_fAmplitude = value; }
        }

        /// <summary>
        /// waves amplitude rate
        /// </summary>
        /// <returns></returns>
        public override float AmplitudeRate
        {
            get { return m_fAmplitudeRate; }
            set { m_fAmplitudeRate = value; }
        }

		protected int Waves
		{
			get { return m_nWaves; }
			set { m_nWaves = value; }
		}
        #region Constructors

        public CCWavesTiles3D()
        {
        }

        /// <summary>
        /// creates the action with a number of waves, the waves amplitude, the grid size and the duration
        /// </summary>
		public CCWavesTiles3D(float duration, CCGridSize gridSize, int waves = 0, float amplitude = 0) : base(duration, gridSize)
        {
            InitCCWavesTiles3D(waves, amplitude);
        }

        // Perform deep copy of CCWavesTiles3D
        public CCWavesTiles3D(CCWavesTiles3D wavesTiles) : base(wavesTiles)
        {
            InitCCWavesTiles3D(wavesTiles.m_nWaves, wavesTiles.m_fAmplitude);
        }

        /// <summary>
        ///  initializes the action with a number of waves, the waves amplitude, the grid size and the duration 
        /// </summary>
        private void InitCCWavesTiles3D(int waves, float amplitude)
        {
            m_nWaves = waves;
            m_fAmplitude = amplitude;
            m_fAmplitudeRate = 1.0f;
        }

        #endregion Constructors


        public override object Copy(ICCCopyable pZone)
        {
            return new CCWavesTiles3D(this);
        }

        public override void Update(float time)
        {
            int i, j;

            for (i = 0; i < m_sGridSize.X; i++)
            {
                for (j = 0; j < m_sGridSize.Y; j++)
                {
                    CCQuad3 coords = OriginalTile(i, j);

                    coords.BottomLeft.Z = ((float) Math.Sin(time * (float) Math.PI * m_nWaves * 2 +
                                                            (coords.BottomLeft.Y + coords.BottomLeft.X) * .01f) *
                                           m_fAmplitude * m_fAmplitudeRate);
                    coords.BottomRight.Z = coords.BottomLeft.Z;
                    coords.TopLeft.Z = coords.BottomLeft.Z;
                    coords.TopRight.Z = coords.BottomLeft.Z;

                    SetTile(i, j, ref coords);
                }
            }
        }
    }
}